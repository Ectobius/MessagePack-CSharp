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
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(6)
            {
                {typeof(System.Collections.Generic.List<ConfigurationTest.Pet>), 0 },
                {typeof(System.Int32[]), 1 },
                {typeof(ConfigurationTest.ExternalObject), 2 },
                {typeof(ConfigurationTest.Pet), 3 },
                {typeof(ConfigurationTest.Person), 4 },
                {typeof(ConfigurationTest.SuperPet), 5 },
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
                    formatter = new global::MessagePack.Formatters.ListFormatter<ConfigurationTest.Pet>();
                    break;
                        case 1:
                    formatter = new global::MessagePack.Formatters.ArrayFormatter<System.Int32>();
                    break;
                        case 2:
                    formatter = new ConfigurationTest.Formatters.ExternalObjectFormatter(ModelFactory);
                    break;
                        case 3:
                    formatter = new ConfigurationTest.Formatters.PetFormatter(ModelFactory);
                    break;
                        case 4:
                    formatter = new ConfigurationTest.Formatters.PersonFormatter(ModelFactory);
                    break;
                        case 5:
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
            { 0, typeof(ConfigurationTest.ExternalObject) },
            { 65, typeof(ConfigurationTest.Pet) },
            { 64, typeof(ConfigurationTest.Person) },
            { 66, typeof(ConfigurationTest.SuperPet) },
        };
    }


    public sealed class ExternalObjectFormatter : global::MessagePack.Formatters.IMessagePackFormatter<ConfigurationTest.ExternalObject>, IMessagePackUntypedFormatter
    {
        private const int TypeId = 0;

        private global::MessagePack.ModelCreation.IModelFactory _modelFactory;

        public ExternalObjectFormatter(global::MessagePack.ModelCreation.IModelFactory modelFactory)
        {
            this._modelFactory = modelFactory;
        }

        public int Serialize(ref byte[] bytes, int offset, ConfigurationTest.ExternalObject value, global::MessagePack.IFormatterResolver formatterResolver, SerializationContext context)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }            

            var startOffset = offset;

            if (context.CheckIfExternal(value))
            {
                var externalObjectId = context.PutToExternalObjects(value);
                offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 2);
                offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, (int)global::MessagePack.ModelSerialization.ReservedTypes.ExternalReference);
                offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, externalObjectId);
                return offset - startOffset;
            }


            offset += global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            return offset - startOffset;

        }

        public ConfigurationTest.ExternalObject Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
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

            if (writtedTypeId == (int) global::MessagePack.ModelSerialization.ReservedTypes.ExternalReference)
            {
                var referencedObjectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                offset += readSize;
                readSize = offset - startOffset;

                if (context.ExternalObjectsByIds != null && context.ExternalObjectsByIds.ContainsKey(referencedObjectId))
                {
                    return (ConfigurationTest.ExternalObject) context.ExternalObjectsByIds[referencedObjectId];
                }

                return null;
            }

            if (writtedTypeId == (int) global::MessagePack.ModelSerialization.ReservedTypes.Reference)
            {
                var referencedObjectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                offset += readSize;
                readSize = offset - startOffset;
                
                if (context.DeserializedObjects.ContainsKey(referencedObjectId))
                {
                    return (ConfigurationTest.ExternalObject) context.DeserializedObjects[referencedObjectId];
                }
                return null;
            }

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
                return (ConfigurationTest.ExternalObject) formatter.Deserialize(bytes, offset, formatterResolver, out readSize, context);
            }

            var objectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            offset += readSize;


            for (int i = 0; i < length - 2; i++)
            {
                var key = i;

                switch (key)
                {
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                offset += readSize;
            }

            readSize = offset - startOffset;

            
            return null;

        }

        public int Serialize(ref byte[] bytes, int offset, object value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            return Serialize(ref bytes, offset, (ConfigurationTest.ExternalObject) value, formatterResolver, context);
        }

        object IMessagePackFormatter<object>.Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            return Deserialize(bytes, offset, formatterResolver, out readSize, context);
        }
    }


    public sealed class PetFormatter : global::MessagePack.Formatters.IMessagePackFormatter<ConfigurationTest.Pet>, IMessagePackUntypedFormatter
    {
        private const int TypeId = 65;

        private global::MessagePack.ModelCreation.IModelFactory _modelFactory;

        public PetFormatter(global::MessagePack.ModelCreation.IModelFactory modelFactory)
        {
            this._modelFactory = modelFactory;
        }

        public int Serialize(ref byte[] bytes, int offset, ConfigurationTest.Pet value, global::MessagePack.IFormatterResolver formatterResolver, SerializationContext context)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }            

            var startOffset = offset;

            if (context.CheckIfExternal(value))
            {
                var externalObjectId = context.PutToExternalObjects(value);
                offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 2);
                offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, (int)global::MessagePack.ModelSerialization.ReservedTypes.ExternalReference);
                offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, externalObjectId);
                return offset - startOffset;
            }

            if (value.GetType() != typeof(ConfigurationTest.Pet))
            {
                if (formatterResolver is IUntypedFormatterResolver untypedFormatterResolver)
                {
                    var derivedTypeFormatter = untypedFormatterResolver.GetFormatter(value.GetType());
                    return derivedTypeFormatter.Serialize(ref bytes, offset, value, formatterResolver, context);
                }
            }


            if (context != null && context.SerializedObjects.ContainsKey(value))
            {
                offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 2);
                offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, (int) global::MessagePack.ModelSerialization.ReservedTypes.Reference);
                offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, context.SerializedObjects[value]);
                return offset - startOffset;
            }

            int objectId = context.PutToSerialized(value);


            offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 5);

            offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, TypeId);
            offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, objectId);

            offset += formatterResolver.GetFormatterWithVerify<System.String>().Serialize(ref bytes, offset, value.Name, formatterResolver, context);
            offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.Power);
            offset += formatterResolver.GetFormatterWithVerify<ConfigurationTest.ExternalObject>().Serialize(ref bytes, offset, value.ExternalObject, formatterResolver, context);
            return offset - startOffset;
        }

        public ConfigurationTest.Pet Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
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

            if (writtedTypeId == (int) global::MessagePack.ModelSerialization.ReservedTypes.ExternalReference)
            {
                var referencedObjectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                offset += readSize;
                readSize = offset - startOffset;

                if (context.ExternalObjectsByIds != null && context.ExternalObjectsByIds.ContainsKey(referencedObjectId))
                {
                    return (ConfigurationTest.Pet) context.ExternalObjectsByIds[referencedObjectId];
                }

                return null;
            }

            if (writtedTypeId == (int) global::MessagePack.ModelSerialization.ReservedTypes.Reference)
            {
                var referencedObjectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                offset += readSize;
                readSize = offset - startOffset;
                
                if (context.DeserializedObjects.ContainsKey(referencedObjectId))
                {
                    return (ConfigurationTest.Pet) context.DeserializedObjects[referencedObjectId];
                }
                return null;
            }

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
                return (ConfigurationTest.Pet) formatter.Deserialize(bytes, offset, formatterResolver, out readSize, context);
            }

            var objectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            offset += readSize;

            var __Name__ = default(System.String);
            var __Power__ = default(System.Single);
            var __ExternalObject__ = default(ConfigurationTest.ExternalObject);

            for (int i = 0; i < length - 2; i++)
            {
                var key = i;

                switch (key)
                {
                    case 0:
                        __Name__ = formatterResolver.GetFormatterWithVerify<System.String>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        break;
                    case 1:
                        __Power__ = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
                        break;
                    case 2:
                        __ExternalObject__ = formatterResolver.GetFormatterWithVerify<ConfigurationTest.ExternalObject>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
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
            ____result.ExternalObject = __ExternalObject__;

            context.DeserializedObjects[objectId] = ____result;

            return ____result;

        }

        public int Serialize(ref byte[] bytes, int offset, object value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            return Serialize(ref bytes, offset, (ConfigurationTest.Pet) value, formatterResolver, context);
        }

        object IMessagePackFormatter<object>.Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            return Deserialize(bytes, offset, formatterResolver, out readSize, context);
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

        public int Serialize(ref byte[] bytes, int offset, ConfigurationTest.Person value, global::MessagePack.IFormatterResolver formatterResolver, SerializationContext context)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }            

            var startOffset = offset;

            if (context.CheckIfExternal(value))
            {
                var externalObjectId = context.PutToExternalObjects(value);
                offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 2);
                offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, (int)global::MessagePack.ModelSerialization.ReservedTypes.ExternalReference);
                offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, externalObjectId);
                return offset - startOffset;
            }

            if (value.GetType() != typeof(ConfigurationTest.Person))
            {
                if (formatterResolver is IUntypedFormatterResolver untypedFormatterResolver)
                {
                    var derivedTypeFormatter = untypedFormatterResolver.GetFormatter(value.GetType());
                    return derivedTypeFormatter.Serialize(ref bytes, offset, value, formatterResolver, context);
                }
            }


            if (context != null && context.SerializedObjects.ContainsKey(value))
            {
                offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 2);
                offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, (int) global::MessagePack.ModelSerialization.ReservedTypes.Reference);
                offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, context.SerializedObjects[value]);
                return offset - startOffset;
            }

            int objectId = context.PutToSerialized(value);


            offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 8);

            offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, TypeId);
            offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, objectId);

            offset += formatterResolver.GetFormatterWithVerify<System.String>().Serialize(ref bytes, offset, value.Name, formatterResolver, context);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Age);
            offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.Height);
            offset += formatterResolver.GetFormatterWithVerify<System.Collections.Generic.List<ConfigurationTest.Pet>>().Serialize(ref bytes, offset, value.Pets, formatterResolver, context);
            offset += formatterResolver.GetFormatterWithVerify<System.Int32[]>().Serialize(ref bytes, offset, value.Numbers, formatterResolver, context);
            offset += formatterResolver.GetFormatterWithVerify<ConfigurationTest.ExternalObject>().Serialize(ref bytes, offset, value.ExternalObject, formatterResolver, context);
            return offset - startOffset;
        }

        public ConfigurationTest.Person Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
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

            if (writtedTypeId == (int) global::MessagePack.ModelSerialization.ReservedTypes.ExternalReference)
            {
                var referencedObjectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                offset += readSize;
                readSize = offset - startOffset;

                if (context.ExternalObjectsByIds != null && context.ExternalObjectsByIds.ContainsKey(referencedObjectId))
                {
                    return (ConfigurationTest.Person) context.ExternalObjectsByIds[referencedObjectId];
                }

                return null;
            }

            if (writtedTypeId == (int) global::MessagePack.ModelSerialization.ReservedTypes.Reference)
            {
                var referencedObjectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                offset += readSize;
                readSize = offset - startOffset;
                
                if (context.DeserializedObjects.ContainsKey(referencedObjectId))
                {
                    return (ConfigurationTest.Person) context.DeserializedObjects[referencedObjectId];
                }
                return null;
            }

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
                return (ConfigurationTest.Person) formatter.Deserialize(bytes, offset, formatterResolver, out readSize, context);
            }

            var objectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            offset += readSize;

            var __Name__ = default(System.String);
            var __Age__ = default(System.Int32);
            var __Height__ = default(System.Single);
            var __Pets__ = default(System.Collections.Generic.List<ConfigurationTest.Pet>);
            var __Numbers__ = default(System.Int32[]);
            var __ExternalObject__ = default(ConfigurationTest.ExternalObject);

            for (int i = 0; i < length - 2; i++)
            {
                var key = i;

                switch (key)
                {
                    case 0:
                        __Name__ = formatterResolver.GetFormatterWithVerify<System.String>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        break;
                    case 1:
                        __Age__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 2:
                        __Height__ = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
                        break;
                    case 3:
                        __Pets__ = formatterResolver.GetFormatterWithVerify<System.Collections.Generic.List<ConfigurationTest.Pet>>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        break;
                    case 4:
                        __Numbers__ = formatterResolver.GetFormatterWithVerify<System.Int32[]>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        break;
                    case 5:
                        __ExternalObject__ = formatterResolver.GetFormatterWithVerify<ConfigurationTest.ExternalObject>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
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
            ____result.Pets = __Pets__;
            ____result.Numbers = __Numbers__;
            ____result.ExternalObject = __ExternalObject__;

            context.DeserializedObjects[objectId] = ____result;

            return ____result;

        }

        public int Serialize(ref byte[] bytes, int offset, object value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            return Serialize(ref bytes, offset, (ConfigurationTest.Person) value, formatterResolver, context);
        }

        object IMessagePackFormatter<object>.Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            return Deserialize(bytes, offset, formatterResolver, out readSize, context);
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

        public int Serialize(ref byte[] bytes, int offset, ConfigurationTest.SuperPet value, global::MessagePack.IFormatterResolver formatterResolver, SerializationContext context)
        {
            if (value == null)
            {
                return global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            }            

            var startOffset = offset;

            if (context.CheckIfExternal(value))
            {
                var externalObjectId = context.PutToExternalObjects(value);
                offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 2);
                offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, (int)global::MessagePack.ModelSerialization.ReservedTypes.ExternalReference);
                offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, externalObjectId);
                return offset - startOffset;
            }

            if (value.GetType() != typeof(ConfigurationTest.SuperPet))
            {
                if (formatterResolver is IUntypedFormatterResolver untypedFormatterResolver)
                {
                    var derivedTypeFormatter = untypedFormatterResolver.GetFormatter(value.GetType());
                    return derivedTypeFormatter.Serialize(ref bytes, offset, value, formatterResolver, context);
                }
            }


            if (context != null && context.SerializedObjects.ContainsKey(value))
            {
                offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 2);
                offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, (int) global::MessagePack.ModelSerialization.ReservedTypes.Reference);
                offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, context.SerializedObjects[value]);
                return offset - startOffset;
            }

            int objectId = context.PutToSerialized(value);


            offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 6);

            offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, TypeId);
            offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, objectId);

            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Kind);
            offset += formatterResolver.GetFormatterWithVerify<System.String>().Serialize(ref bytes, offset, value.Name, formatterResolver, context);
            offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.Power);
            offset += formatterResolver.GetFormatterWithVerify<ConfigurationTest.ExternalObject>().Serialize(ref bytes, offset, value.ExternalObject, formatterResolver, context);
            return offset - startOffset;
        }

        public ConfigurationTest.SuperPet Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
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

            if (writtedTypeId == (int) global::MessagePack.ModelSerialization.ReservedTypes.ExternalReference)
            {
                var referencedObjectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                offset += readSize;
                readSize = offset - startOffset;

                if (context.ExternalObjectsByIds != null && context.ExternalObjectsByIds.ContainsKey(referencedObjectId))
                {
                    return (ConfigurationTest.SuperPet) context.ExternalObjectsByIds[referencedObjectId];
                }

                return null;
            }

            if (writtedTypeId == (int) global::MessagePack.ModelSerialization.ReservedTypes.Reference)
            {
                var referencedObjectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                offset += readSize;
                readSize = offset - startOffset;
                
                if (context.DeserializedObjects.ContainsKey(referencedObjectId))
                {
                    return (ConfigurationTest.SuperPet) context.DeserializedObjects[referencedObjectId];
                }
                return null;
            }

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
                return (ConfigurationTest.SuperPet) formatter.Deserialize(bytes, offset, formatterResolver, out readSize, context);
            }

            var objectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            offset += readSize;

            var __Kind__ = default(System.Int32);
            var __Name__ = default(System.String);
            var __Power__ = default(System.Single);
            var __ExternalObject__ = default(ConfigurationTest.ExternalObject);

            for (int i = 0; i < length - 2; i++)
            {
                var key = i;

                switch (key)
                {
                    case 0:
                        __Kind__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 1:
                        __Name__ = formatterResolver.GetFormatterWithVerify<System.String>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        break;
                    case 2:
                        __Power__ = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
                        break;
                    case 3:
                        __ExternalObject__ = formatterResolver.GetFormatterWithVerify<ConfigurationTest.ExternalObject>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
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
            ____result.ExternalObject = __ExternalObject__;

            context.DeserializedObjects[objectId] = ____result;

            return ____result;

        }

        public int Serialize(ref byte[] bytes, int offset, object value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            return Serialize(ref bytes, offset, (ConfigurationTest.SuperPet) value, formatterResolver, context);
        }

        object IMessagePackFormatter<object>.Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            return Deserialize(bytes, offset, formatterResolver, out readSize, context);
        }
    }

}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
