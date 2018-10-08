using MessagePack.Internal;
using System;
using System.IO;
using MessagePack.Formatters;

namespace MessagePack
{
    /// <summary>
    /// High-Level API of MessagePack for C#.
    /// </summary>
    public static partial class MessagePackSerializer
    {
        static IFormatterResolver defaultResolver;

        /// <summary>
        /// FormatterResolver that used resolver less overloads. If does not set it, used StandardResolver.
        /// </summary>
        public static IFormatterResolver DefaultResolver
        {
            get
            {
                if (defaultResolver == null)
                {
                    defaultResolver = MessagePack.Resolvers.StandardResolver.Instance;
                }

                return defaultResolver;
            }
        }

        /// <summary>
        /// Is resolver decided?
        /// </summary>
        public static bool IsInitialized
        {
            get
            {
                return defaultResolver != null;
            }
        }

        /// <summary>
        /// Set default resolver of MessagePackSerializer APIs.
        /// </summary>
        /// <param name="resolver"></param>
        public static void SetDefaultResolver(IFormatterResolver resolver)
        {
            defaultResolver = resolver;
        }

        /// <summary>
        /// Serialize to binary with default resolver.
        /// </summary>
        public static byte[] Serialize<T>(T obj, SerializationOptions options = null)
        {
            return Serialize(obj, defaultResolver, options);
        }

        /// <summary>
        /// Serialize to binary with specified resolver.
        /// </summary>
        public static byte[] Serialize<T>(T obj, IFormatterResolver resolver, SerializationOptions options = null)
        {
            if (resolver == null) resolver = DefaultResolver;
            var formatter = resolver.GetFormatterWithVerify<T>();

            var buffer = InternalMemoryPool.GetBuffer();
            var len = SerializeByFormatter(formatter, ref buffer, 0, obj, resolver, options);

            // do not return MemoryPool.Buffer.
            return MessagePackBinary.FastCloneWithResize(buffer, len);
        }

        /// <summary>
        /// Serialize to binary. Get the raw memory pool byte[]. The result can not share across thread and can not hold, so use quickly.
        /// </summary>
        public static ArraySegment<byte> SerializeUnsafe<T>(T obj, SerializationOptions options = null)
        {
            return SerializeUnsafe(obj, defaultResolver, options);
        }

        /// <summary>
        /// Serialize to binary with specified resolver. Get the raw memory pool byte[]. The result can not share across thread and can not hold, so use quickly.
        /// </summary>
        public static ArraySegment<byte> SerializeUnsafe<T>(T obj, IFormatterResolver resolver, SerializationOptions options = null)
        {
            if (resolver == null) resolver = DefaultResolver;
            var formatter = resolver.GetFormatterWithVerify<T>();

            var buffer = InternalMemoryPool.GetBuffer();
            var len = SerializeByFormatter(formatter, ref buffer, 0, obj, resolver, options);

            // return raw memory pool, unsafe!
            return new ArraySegment<byte>(buffer, 0, len);
        }

        /// <summary>
        /// Serialize to stream.
        /// </summary>
        public static void Serialize<T>(Stream stream, T obj, SerializationOptions options = null)
        {
            Serialize(stream, obj, defaultResolver);
        }

        /// <summary>
        /// Serialize to stream with specified resolver.
        /// </summary>
        public static void Serialize<T>(Stream stream, T obj, IFormatterResolver resolver, SerializationOptions options = null)
        {
            if (resolver == null) resolver = DefaultResolver;
            var formatter = resolver.GetFormatterWithVerify<T>();

            var buffer = InternalMemoryPool.GetBuffer();
            var len = SerializeByFormatter(formatter, ref buffer, 0, obj, resolver, options);

            // do not need resize.
            stream.Write(buffer, 0, len);
        }

        /// <summary>
        /// Reflect of resolver.GetFormatterWithVerify[T].Serialize.
        /// </summary>
        public static int Serialize<T>(ref byte[] bytes, int offset, T value, IFormatterResolver resolver, SerializationOptions options = null)
        {
            var formatter = resolver.GetFormatterWithVerify<T>();
            return SerializeByFormatter(formatter, ref bytes, offset, value, resolver, options);
        }

#if NETSTANDARD

        /// <summary>
        /// Serialize to stream(async).
        /// </summary>
        public static System.Threading.Tasks.Task SerializeAsync<T>(Stream stream, T obj)
        {
            return SerializeAsync(stream, obj, defaultResolver);
        }

        /// <summary>
        /// Serialize to stream(async) with specified resolver.
        /// </summary>
        public static async System.Threading.Tasks.Task SerializeAsync<T>(Stream stream, T obj, IFormatterResolver resolver, SerializationOptions options = null)
        {
            if (resolver == null) resolver = DefaultResolver;
            var formatter = resolver.GetFormatterWithVerify<T>();

            var rentBuffer = BufferPool.Default.Rent();
            try
            {
                var buffer = rentBuffer;
                var len = SerializeByFormatter(formatter, ref buffer, 0, obj, resolver, options);

                // do not need resize.
                await stream.WriteAsync(buffer, 0, len).ConfigureAwait(false);
            }
            finally
            {
                BufferPool.Default.Return(rentBuffer);
            }
        }

#endif

        private static int SerializeByFormatter<T>(IMessagePackFormatter<T> formatter, ref byte[] buffer, int offset,
            T value, IFormatterResolver resolver, SerializationOptions options)
        {
            var context = InternalContextPool.GetSerializationContext();

            if (options != null)
            {
                context.ExternalReferenceChecker = options.ExternalReferenceChecker;
                context.ExternalObjectsByIds = options.ExternalObjectsByIds;
                context.ExtraData = options.ExtraData;
            }

            return formatter.Serialize(ref buffer, offset, value, resolver, context);
        }

        public static T Deserialize<T>(byte[] bytes, DeserializationOptions options = null)
        {
            return Deserialize<T>(bytes, defaultResolver, options);
        }

        public static T Deserialize<T>(byte[] bytes, IFormatterResolver resolver, DeserializationOptions options = null)
        {
            if (resolver == null) resolver = DefaultResolver;
            var formatter = resolver.GetFormatterWithVerify<T>();

            int readSize;
            return DeserializeByFormatter(formatter, bytes, 0, resolver, out readSize, options);
        }

        public static T Deserialize<T>(ArraySegment<byte> bytes, DeserializationOptions options = null)
        {
            return Deserialize<T>(bytes, defaultResolver, options);
        }

        public static T Deserialize<T>(ArraySegment<byte> bytes, IFormatterResolver resolver, DeserializationOptions options = null)
        {
            if (resolver == null) resolver = DefaultResolver;
            var formatter = resolver.GetFormatterWithVerify<T>();

            int readSize;
            return DeserializeByFormatter(formatter, bytes.Array, bytes.Offset, resolver, out readSize, options);
        }

        public static T Deserialize<T>(Stream stream, DeserializationOptions options = null)
        {
            return Deserialize<T>(stream, defaultResolver, options);
        }

        public static T Deserialize<T>(Stream stream, IFormatterResolver resolver, DeserializationOptions options = null)
        {
            return Deserialize<T>(stream, resolver, false, options);
        }

        public static T Deserialize<T>(Stream stream, bool readStrict, DeserializationOptions options = null)
        {
            return Deserialize<T>(stream, defaultResolver, readStrict, options);
        }

        public static T Deserialize<T>(Stream stream, IFormatterResolver resolver, bool readStrict, DeserializationOptions options = null)
        {
            if (resolver == null) resolver = DefaultResolver;
            var formatter = resolver.GetFormatterWithVerify<T>();

            if (!readStrict)
            {
#if NETSTANDARD && !NET45

                var ms = stream as MemoryStream;
                if (ms != null)
                {
                    // optimize for MemoryStream
                    ArraySegment<byte> buffer;
                    if (ms.TryGetBuffer(out buffer))
                    {
                        int readSize;
                        return DeserializeByFormatter(formatter, buffer.Array, buffer.Offset, resolver, out readSize, options);
                    }
                }
#endif

                // no else.
                {
                    var buffer = InternalMemoryPool.GetBuffer();

                    FillFromStream(stream, ref buffer);

                    int readSize;
                    return DeserializeByFormatter(formatter, buffer, 0, resolver, out readSize, options);
                }
            }
            else
            {
                int _;
                var bytes = MessagePackBinary.ReadMessageBlockFromStreamUnsafe(stream, false, out _);
                int readSize;
                return DeserializeByFormatter(formatter, bytes, 0, resolver, out readSize, options);
            }
        }

        /// <summary>
        /// Reflect of resolver.GetFormatterWithVerify[T].Deserialize.
        /// </summary>
        public static T Deserialize<T>(byte[] bytes, int offset, IFormatterResolver resolver, out int readSize, DeserializationContext context)
        {
            return resolver.GetFormatterWithVerify<T>().Deserialize(bytes, offset, resolver, out readSize, context);
        }

#if NETSTANDARD

        public static System.Threading.Tasks.Task<T> DeserializeAsync<T>(Stream stream, DeserializationOptions options = null)
        {
            return DeserializeAsync<T>(stream, defaultResolver);
        }

        // readStrict async read is too slow(many Task garbage) so I don't provide async option.

        public static async System.Threading.Tasks.Task<T> DeserializeAsync<T>(Stream stream, IFormatterResolver resolver, DeserializationOptions options = null)
        {
            var rentBuffer = BufferPool.Default.Rent();
            var buf = rentBuffer;
            try
            {
                int length = 0;
                int read;
                while ((read = await stream.ReadAsync(buf, length, buf.Length - length).ConfigureAwait(false)) > 0)
                {
                    length += read;
                    if (length == buf.Length)
                    {
                        MessagePackBinary.FastResize(ref buf, length * 2);
                    }
                }

                return Deserialize<T>(buf, resolver);
            }
            finally
            {
                BufferPool.Default.Return(rentBuffer);
            }
        }

#endif

        public static void Populate<T>(ref T value, byte[] bytes, DeserializationOptions options = null)
        {
            Populate(ref value, bytes, defaultResolver, options);
        }

        public static void Populate<T>(ref T value, byte[] bytes, IFormatterResolver resolver, DeserializationOptions options = null)
        {
            if (resolver == null) resolver = DefaultResolver;
            var formatter = resolver.GetFormatterWithVerify<T>();

            int readSize;
            PopulateByFormatter(ref value, formatter, bytes, 0, resolver, out readSize, options);
        }

        private static T DeserializeByFormatter<T>(IMessagePackFormatter<T> formatter, byte[] bytes, int offset, IFormatterResolver resolver,
            out int readSize, DeserializationOptions options)
        {
            var context = InternalContextPool.GetDeserializationContext();

            if (options != null)
            {
                context.ExternalObjectsByIds = options.ExternalObjectsByIds;
                context.ExtraData = options.ExtraData;
            }

            return formatter.Deserialize(bytes, offset, resolver, out readSize, context);
        }

        private static void PopulateByFormatter<T>(ref T value, IMessagePackFormatter<T> formatter, byte[] bytes, int offset, IFormatterResolver resolver,
            out int readSize, DeserializationOptions options)
        {
            var context = InternalContextPool.GetDeserializationContext();

            if (options != null)
            {
                context.ExternalObjectsByIds = options.ExternalObjectsByIds;
                context.ExtraData = options.ExtraData;
            }

            if (formatter is IMessagePackFormatterWithPopulate<T> formatterWithPopulate)
            {
                formatterWithPopulate.Populate(ref value, bytes, offset, resolver, out readSize, context);
            }
            else
            {
                throw new Exception($"Formatter for {typeof(T)} doesn't support populating");
            }
        }

        static int FillFromStream(Stream input, ref byte[] buffer)
        {
            int length = 0;
            int read;
            while ((read = input.Read(buffer, length, buffer.Length - length)) > 0)
            {
                length += read;
                if (length == buffer.Length)
                {
                    MessagePackBinary.FastResize(ref buffer, length * 2);
                }
            }

            return length;
        }
    }
}

namespace MessagePack.Internal
{
    internal static class InternalMemoryPool
    {
        [ThreadStatic]
        static byte[] buffer = null;

        public static byte[] GetBuffer()
        {
            if (buffer == null)
            {
                buffer = new byte[65536];
            }
            return buffer;
        }
    }

    internal static class InternalContextPool
    {
        [ThreadStatic]
        static SerializationContext _serializationContext;

        [ThreadStatic]
        static DeserializationContext _deserializationContext;

        public static SerializationContext GetSerializationContext()
        {
            if (_serializationContext == null)
            {
                _serializationContext = new SerializationContext();
            }

            _serializationContext.Reset();

            return _serializationContext;
        }

        public static DeserializationContext GetDeserializationContext()
        {
            if (_deserializationContext == null)
            {
                _deserializationContext = new DeserializationContext();
            }

            _deserializationContext.Reset();

            return _deserializationContext;
        }
    }
}