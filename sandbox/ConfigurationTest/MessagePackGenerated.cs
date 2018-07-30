#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace ConfigurationTest.Resolvers
{
    using System;
    using MessagePack;

    public class GeneratedResolver : global::MessagePack.IFormatterResolver
    {
        public static readonly global::MessagePack.IFormatterResolver Instance = new GeneratedResolver();

        GeneratedResolver()
        {

        }

        public global::MessagePack.Formatters.IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly global::MessagePack.Formatters.IMessagePackFormatter<T> formatter;

            static FormatterCache()
            {
                var f = GeneratedResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    formatter = (global::MessagePack.Formatters.IMessagePackFormatter<T>)f;
                }
            }
        }
    }

    internal static class GeneratedResolverGetFormatterHelper
    {
        static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static GeneratedResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(4)
            {
                {typeof(System.Collections.Generic.List<ConfigurationTest.Pet>), 0 },
                {typeof(System.Int32[]), 1 },
                {typeof(ConfigurationTest.Pet), 2 },
                {typeof(ConfigurationTest.Person), 3 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key)) return null;

            switch (key)
            {
                case 0: return new global::MessagePack.Formatters.ListFormatter<ConfigurationTest.Pet>();
                case 1: return new global::MessagePack.Formatters.ArrayFormatter<System.Int32>();
                case 2: return new ConfigurationTest.Formatters.PetFormatter();
                case 3: return new ConfigurationTest.Formatters.PersonFormatter();
                default: return null;
            }
        }
    }
}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612



#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace ConfigurationTest.Formatters
{
    using System;
    using MessagePack;


    public sealed class PetFormatter : global::MessagePack.Formatters.IMessagePackFormatter<ConfigurationTest.Pet>
    {

        public int Serialize(ref byte[] bytes, int offset, ConfigurationTest.Pet value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 2);
            offset += formatterResolver.GetFormatterWithVerify<System.String>().Serialize(ref bytes, offset, value.Name, formatterResolver);
            offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.Power);
            return offset - startOffset;
        }

        public ConfigurationTest.Pet Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
            offset += readSize;

            var __Name__ = default(System.String);
            var __Power__ = default(System.Single);

            for (int i = 0; i < length; i++)
            {
                var key = i;

                switch (key)
                {
                    case 0:
                        __Name__ = formatterResolver.GetFormatterWithVerify<System.String>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __Power__ = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new ConfigurationTest.Pet();
            ____result.Name = __Name__;
            ____result.Power = __Power__;
            return ____result;
        }
    }


    public sealed class PersonFormatter : global::MessagePack.Formatters.IMessagePackFormatter<ConfigurationTest.Person>
    {

        public int Serialize(ref byte[] bytes, int offset, ConfigurationTest.Person value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 5);
            offset += formatterResolver.GetFormatterWithVerify<System.String>().Serialize(ref bytes, offset, value.Name, formatterResolver);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Age);
            offset += formatterResolver.GetFormatterWithVerify<System.Collections.Generic.List<ConfigurationTest.Pet>>().Serialize(ref bytes, offset, value.Pets, formatterResolver);
            offset += formatterResolver.GetFormatterWithVerify<System.Int32[]>().Serialize(ref bytes, offset, value.Numbers, formatterResolver);
            offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.Height);
            return offset - startOffset;
        }

        public ConfigurationTest.Person Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
            offset += readSize;

            var __Name__ = default(System.String);
            var __Age__ = default(System.Int32);
            var __Pets__ = default(System.Collections.Generic.List<ConfigurationTest.Pet>);
            var __Numbers__ = default(System.Int32[]);
            var __Height__ = default(System.Single);

            for (int i = 0; i < length; i++)
            {
                var key = i;

                switch (key)
                {
                    case 0:
                        __Name__ = formatterResolver.GetFormatterWithVerify<System.String>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 1:
                        __Age__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 2:
                        __Pets__ = formatterResolver.GetFormatterWithVerify<System.Collections.Generic.List<ConfigurationTest.Pet>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 3:
                        __Numbers__ = formatterResolver.GetFormatterWithVerify<System.Int32[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 4:
                        __Height__ = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = new ConfigurationTest.Person();
            ____result.Name = __Name__;
            ____result.Age = __Age__;
            ____result.Pets = __Pets__;
            ____result.Numbers = __Numbers__;
            ____result.Height = __Height__;
            return ____result;
        }
    }

}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
