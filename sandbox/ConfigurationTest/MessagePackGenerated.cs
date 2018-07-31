#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace ConfigurationTest.Resolvers
{
    using System;
    using System.Collections.Generic;
    using MessagePack;
    using MessagePack.Formatters;

    public class GeneratedResolver : global::MessagePack.IFormatterResolver, global::MessagePack.IUntypedFormatterResolver
    {
        public static readonly global::MessagePack.IFormatterResolver Instance = new GeneratedResolver();        

        GeneratedResolver()
        {

        }

        public static void SetModelFactory(global::MessagePack.ModelCreation.IModelFactory modelFactory)
        {
            GeneratedResolverGetFormatterHelper.ModelFactory = modelFactory;
        }

        public global::MessagePack.Formatters.IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        public IMessagePackUntypedFormatter GetFormatter(Type type)
        {
            return (IMessagePackUntypedFormatter) GeneratedResolverGetFormatterHelper.GetFormatter(type);
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

        internal static global::MessagePack.ModelCreation.IModelFactory ModelFactory = new MessagePack.ModelCreation.DefaultModelFactory();
        static readonly Dictionary<Type, global::MessagePack.Formatters.IMessagePackFormatter> Cache;

        static GeneratedResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(5)
            {
                {typeof(System.Int32[]), 0 },
                {typeof(System.Collections.Generic.List<ConfigurationTest.Pet>), 1 },
                {typeof(ConfigurationTest.Pet), 2 },
                {typeof(ConfigurationTest.Person), 3 },
                {typeof(ConfigurationTest.SuperPet), 4 },
            };

            Cache = new Dictionary<Type, global::MessagePack.Formatters.IMessagePackFormatter>();
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key)) return null;
            
            if (Cache.TryGetValue(t, out var cachedFormatter))
            {
                return cachedFormatter;
            }

            object formatter = null;
            switch (key)
            {
                    case 0:
                    formatter = new global::MessagePack.Formatters.ArrayFormatter<System.Int32>();
                    break;
                        case 1:
                    formatter = new global::MessagePack.Formatters.ListFormatter<ConfigurationTest.Pet>();
                    break;
                        case 2:
                    formatter = new ConfigurationTest.Formatters.PetFormatter(ModelFactory);
                    break;
                        case 3:
                    formatter = new ConfigurationTest.Formatters.PersonFormatter(ModelFactory);
                    break;
                        case 4:
                    formatter = new ConfigurationTest.Formatters.SuperPetFormatter(ModelFactory);
                    break;
                    default: return null;
            }

            Cache[t] = (IMessagePackFormatter) formatter;
            return formatter;
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
    using System.Collections.Generic;
    using MessagePack;
    using MessagePack.Formatters;

    internal class TypeRegistry
    {
        public static Dictionary<int, Type> Types { get; } = new Dictionary<int, Type>
        {
            { 65, typeof(ConfigurationTest.Pet) },
            { 64, typeof(ConfigurationTest.Person) },
            { 66, typeof(ConfigurationTest.SuperPet) },
        };
    }

    
    public sealed class PetFormatter : global::MessagePack.Formatters.IMessagePackFormatter<ConfigurationTest.Pet>, IMessagePackUntypedFormatter
    {
        private const int TypeId = 65;

        private global::MessagePack.ModelCreation.IModelFactory _modelFactory;

        public PetFormatter(global::MessagePack.ModelCreation.IModelFactory modelFactory)
        {
            this._modelFactory = modelFactory;
        }

        public int Serialize(ref byte[] bytes, int offset, ConfigurationTest.Pet value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }

            if (value.GetType() != typeof(ConfigurationTest.Pet))
            {
                if (formatterResolver is IUntypedFormatterResolver untypedFormatterResolver)
                {
                    var derivedTypeFormatter = untypedFormatterResolver.GetFormatter(value.GetType());
                    return derivedTypeFormatter.Serialize(ref bytes, offset, value, formatterResolver);
                }
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 3);
            offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, TypeId);
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

            var writtedTypeId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            offset += readSize;

            if (writtedTypeId != TypeId)
            {
                var actualType = TypeRegistry.Types[writtedTypeId];
                if (!(formatterResolver is IUntypedFormatterResolver))
                {
                    throw new Exception("In order to deserialize derived types resolver should implement IUntypedFormatterResolver");
                }

                var untypedFormatterResolver = formatterResolver as IUntypedFormatterResolver;
                var formatter = untypedFormatterResolver.GetFormatter(actualType);

                offset = startOffset;
                return (ConfigurationTest.Pet) formatter.Deserialize(bytes, offset, formatterResolver, out readSize);
            }

            var __Name__ = default(System.String);
            var __Power__ = default(System.Single);

            for (int i = 0; i < length - 1; i++)
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

            var ____result = _modelFactory.CreateModel<ConfigurationTest.Pet>(); //new ConfigurationTest.Pet();
            ____result.Name = __Name__;
            ____result.Power = __Power__;
            return ____result;
        }

        public int Serialize(ref byte[] bytes, int offset, object value, IFormatterResolver formatterResolver)
        {
            return Serialize(ref bytes, offset, (ConfigurationTest.Pet) value, formatterResolver);
        }

        object IMessagePackFormatter<object>.Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            return Deserialize(bytes, offset, formatterResolver, out readSize);
        }
    }

    
    public sealed class PersonFormatter : global::MessagePack.Formatters.IMessagePackFormatter<ConfigurationTest.Person>, IMessagePackUntypedFormatter
    {
        private const int TypeId = 64;

        private global::MessagePack.ModelCreation.IModelFactory _modelFactory;

        public PersonFormatter(global::MessagePack.ModelCreation.IModelFactory modelFactory)
        {
            this._modelFactory = modelFactory;
        }

        public int Serialize(ref byte[] bytes, int offset, ConfigurationTest.Person value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }

            if (value.GetType() != typeof(ConfigurationTest.Person))
            {
                if (formatterResolver is IUntypedFormatterResolver untypedFormatterResolver)
                {
                    var derivedTypeFormatter = untypedFormatterResolver.GetFormatter(value.GetType());
                    return derivedTypeFormatter.Serialize(ref bytes, offset, value, formatterResolver);
                }
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 7);
            offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, TypeId);
            offset += formatterResolver.GetFormatterWithVerify<System.String>().Serialize(ref bytes, offset, value.Name, formatterResolver);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Age);
            offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.Height);
            offset += global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            offset += formatterResolver.GetFormatterWithVerify<System.Int32[]>().Serialize(ref bytes, offset, value.Numbers, formatterResolver);
            offset += formatterResolver.GetFormatterWithVerify<System.Collections.Generic.List<ConfigurationTest.Pet>>().Serialize(ref bytes, offset, value.Pets, formatterResolver);
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

            var writtedTypeId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            offset += readSize;

            if (writtedTypeId != TypeId)
            {
                var actualType = TypeRegistry.Types[writtedTypeId];
                if (!(formatterResolver is IUntypedFormatterResolver))
                {
                    throw new Exception("In order to deserialize derived types resolver should implement IUntypedFormatterResolver");
                }

                var untypedFormatterResolver = formatterResolver as IUntypedFormatterResolver;
                var formatter = untypedFormatterResolver.GetFormatter(actualType);

                offset = startOffset;
                return (ConfigurationTest.Person) formatter.Deserialize(bytes, offset, formatterResolver, out readSize);
            }

            var __Name__ = default(System.String);
            var __Age__ = default(System.Int32);
            var __Height__ = default(System.Single);
            var __Numbers__ = default(System.Int32[]);
            var __Pets__ = default(System.Collections.Generic.List<ConfigurationTest.Pet>);

            for (int i = 0; i < length - 1; i++)
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
                        __Height__ = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
                        break;
                    case 4:
                        __Numbers__ = formatterResolver.GetFormatterWithVerify<System.Int32[]>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 5:
                        __Pets__ = formatterResolver.GetFormatterWithVerify<System.Collections.Generic.List<ConfigurationTest.Pet>>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = _modelFactory.CreateModel<ConfigurationTest.Person>(); //new ConfigurationTest.Person();
            ____result.Name = __Name__;
            ____result.Age = __Age__;
            ____result.Height = __Height__;
            ____result.Numbers = __Numbers__;
            ____result.Pets = __Pets__;
            return ____result;
        }

        public int Serialize(ref byte[] bytes, int offset, object value, IFormatterResolver formatterResolver)
        {
            return Serialize(ref bytes, offset, (ConfigurationTest.Person) value, formatterResolver);
        }

        object IMessagePackFormatter<object>.Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            return Deserialize(bytes, offset, formatterResolver, out readSize);
        }
    }

    
    public sealed class SuperPetFormatter : global::MessagePack.Formatters.IMessagePackFormatter<ConfigurationTest.SuperPet>, IMessagePackUntypedFormatter
    {
        private const int TypeId = 66;

        private global::MessagePack.ModelCreation.IModelFactory _modelFactory;

        public SuperPetFormatter(global::MessagePack.ModelCreation.IModelFactory modelFactory)
        {
            this._modelFactory = modelFactory;
        }

        public int Serialize(ref byte[] bytes, int offset, ConfigurationTest.SuperPet value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }

            if (value.GetType() != typeof(ConfigurationTest.SuperPet))
            {
                if (formatterResolver is IUntypedFormatterResolver untypedFormatterResolver)
                {
                    var derivedTypeFormatter = untypedFormatterResolver.GetFormatter(value.GetType());
                    return derivedTypeFormatter.Serialize(ref bytes, offset, value, formatterResolver);
                }
            }
            
            var startOffset = offset;
            offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 4);
            offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, TypeId);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Kind);
            offset += formatterResolver.GetFormatterWithVerify<System.String>().Serialize(ref bytes, offset, value.Name, formatterResolver);
            offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.Power);
            return offset - startOffset;
        }

        public ConfigurationTest.SuperPet Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                return null;
            }

            var startOffset = offset;
            var length = global::MessagePack.MessagePackBinary.ReadArrayHeader(bytes, offset, out readSize);
            offset += readSize;

            var writtedTypeId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            offset += readSize;

            if (writtedTypeId != TypeId)
            {
                var actualType = TypeRegistry.Types[writtedTypeId];
                if (!(formatterResolver is IUntypedFormatterResolver))
                {
                    throw new Exception("In order to deserialize derived types resolver should implement IUntypedFormatterResolver");
                }

                var untypedFormatterResolver = formatterResolver as IUntypedFormatterResolver;
                var formatter = untypedFormatterResolver.GetFormatter(actualType);

                offset = startOffset;
                return (ConfigurationTest.SuperPet) formatter.Deserialize(bytes, offset, formatterResolver, out readSize);
            }

            var __Kind__ = default(System.Int32);
            var __Name__ = default(System.String);
            var __Power__ = default(System.Single);

            for (int i = 0; i < length - 1; i++)
            {
                var key = i;

                switch (key)
                {
                    case 0:
                        __Kind__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 1:
                        __Name__ = formatterResolver.GetFormatterWithVerify<System.String>().Deserialize(bytes, offset, formatterResolver, out readSize);
                        break;
                    case 2:
                        __Power__ = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                offset += readSize;
            }

            readSize = offset - startOffset;

            var ____result = _modelFactory.CreateModel<ConfigurationTest.SuperPet>(); //new ConfigurationTest.SuperPet();
            ____result.Kind = __Kind__;
            ____result.Name = __Name__;
            ____result.Power = __Power__;
            return ____result;
        }

        public int Serialize(ref byte[] bytes, int offset, object value, IFormatterResolver formatterResolver)
        {
            return Serialize(ref bytes, offset, (ConfigurationTest.SuperPet) value, formatterResolver);
        }

        object IMessagePackFormatter<object>.Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            return Deserialize(bytes, offset, formatterResolver, out readSize);
        }
    }

}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
