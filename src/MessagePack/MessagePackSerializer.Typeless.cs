#if NETSTANDARD

using System;
using System.Collections.Generic;
using System.Text;
using MessagePack.Resolvers;
using MessagePack.Formatters;
using System.IO;

namespace MessagePack
{
    // Typeless API
    public static partial class MessagePackSerializer
    {
        public static class Typeless
        {
            static IFormatterResolver defaultResolver = MessagePack.Resolvers.TypelessContractlessStandardResolver.Instance;

            public static void RegisterDefaultResolver(params IFormatterResolver[] resolvers)
            {
                CompositeResolver.Register(resolvers);
                defaultResolver = CompositeResolver.Instance;
            }

            public static byte[] Serialize(object obj, SerializationContext context)
            {
                return MessagePackSerializer.Serialize(obj, defaultResolver, context);
            }

            public static void Serialize(Stream stream, object obj, SerializationContext context)
            {
                MessagePackSerializer.Serialize(stream, obj, defaultResolver, context);
            }

            public static System.Threading.Tasks.Task SerializeAsync(Stream stream, object obj, SerializationContext context)
            {
                return MessagePackSerializer.SerializeAsync(stream, obj, defaultResolver, context);
            }

            public static object Deserialize(byte[] bytes, DeserializationContext context)
            {
                return MessagePackSerializer.Deserialize<object>(bytes, defaultResolver, context);
            }

            public static object Deserialize(Stream stream, DeserializationContext context)
            {
                return MessagePackSerializer.Deserialize<object>(stream, defaultResolver, context);
            }

            public static object Deserialize(Stream stream, bool readStrict, DeserializationContext context)
            {
                return MessagePackSerializer.Deserialize<object>(stream, defaultResolver, readStrict, context);
            }

            public static System.Threading.Tasks.Task<object> DeserializeAsync(Stream stream, DeserializationContext context)
            {
                return MessagePackSerializer.DeserializeAsync<object>(stream, defaultResolver, context);
            }

            class CompositeResolver : IFormatterResolver
            {
                public static readonly CompositeResolver Instance = new CompositeResolver();

                static bool isFreezed = false;
                static IFormatterResolver[] resolvers = new IFormatterResolver[0];

                CompositeResolver()
                {
                }

                public static void Register(params IFormatterResolver[] resolvers)
                {
                    if (isFreezed)
                    {
                        throw new InvalidOperationException("Register must call on startup(before use GetFormatter<T>).");
                    }

                    CompositeResolver.resolvers = resolvers;
                }

                public IMessagePackFormatter<T> GetFormatter<T>()
                {
                    return FormatterCache<T>.formatter;
                }

                static class FormatterCache<T>
                {
                    public static readonly IMessagePackFormatter<T> formatter;

                    static FormatterCache()
                    {
                        isFreezed = true;

                        foreach (var item in resolvers)
                        {
                            var f = item.GetFormatter<T>();
                            if (f != null)
                            {
                                formatter = f;
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}

#endif