#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace BenchmarkCustomized.Resolvers
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
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(9)
            {
                {typeof(System.Collections.Generic.List<TestModels.Pet>), 0 },
                {typeof(System.Int32[]), 1 },
                {typeof(System.Collections.Generic.IList<TestModels.Person>), 2 },
                {typeof(System.Collections.Generic.Dictionary<System.String, TestModels.Pet>), 3 },
                {typeof(TestModels.ExternalObject), 4 },
                {typeof(TestModels.Pet), 5 },
                {typeof(TestModels.Person), 6 },
                {typeof(TestModels.SuperPet), 7 },
                {typeof(TestModels.UltimatePet), 8 },
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
                    formatter = new global::MessagePack.Formatters.ListFormatter<TestModels.Pet>();
                    break;
                        case 1:
                    formatter = new global::MessagePack.Formatters.ArrayFormatter<System.Int32>();
                    break;
                        case 2:
                    formatter = new global::MessagePack.Formatters.InterfaceListFormatter<TestModels.Person>();
                    break;
                        case 3:
                    formatter = new global::MessagePack.Formatters.DictionaryFormatter<System.String, TestModels.Pet>();
                    break;
                        case 4:
                    formatter = new BenchmarkCustomized.Formatters.ExternalObjectFormatter(ModelFactory);
                    break;
                        case 5:
                    formatter = new BenchmarkCustomized.Formatters.PetFormatter(ModelFactory);
                    break;
                        case 6:
                    formatter = new BenchmarkCustomized.Formatters.PersonFormatter(ModelFactory);
                    break;
                        case 7:
                    formatter = new BenchmarkCustomized.Formatters.SuperPetFormatter(ModelFactory);
                    break;
                        case 8:
                    formatter = new BenchmarkCustomized.Formatters.UltimatePetFormatter(ModelFactory);
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

namespace BenchmarkCustomized.Formatters
{
    using System;
    using System.Collections.Generic;
    using MessagePack;
    using MessagePack.Formatters;

    internal class TypeRegistry
    {
        public static Dictionary<int, Type> Types { get; } = new Dictionary<int, Type>
        {
            { 0, typeof(TestModels.ExternalObject) },
            { 65, typeof(TestModels.Pet) },
            { 64, typeof(TestModels.Person) },
            { 66, typeof(TestModels.SuperPet) },
            { 67, typeof(TestModels.UltimatePet) },
        };
    }


    public sealed class ExternalObjectFormatter : global::MessagePack.Formatters.IMessagePackFormatterWithPopulate<TestModels.ExternalObject>, IMessagePackUntypedFormatterWithPopulate, IMessagePackUntypedFormatter
    {
        private const int TypeId = 0;

        private global::MessagePack.ModelCreation.IModelFactory _modelFactory;

        public ExternalObjectFormatter(global::MessagePack.ModelCreation.IModelFactory modelFactory)
        {
            this._modelFactory = modelFactory;
        }

        public int Serialize(ref byte[] bytes, int offset, TestModels.ExternalObject value, global::MessagePack.IFormatterResolver formatterResolver, SerializationContext context)
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

        public TestModels.ExternalObject Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
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
                    return (TestModels.ExternalObject) context.ExternalObjectsByIds[referencedObjectId];
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
                    return (TestModels.ExternalObject) context.DeserializedObjects[referencedObjectId];
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
                return (TestModels.ExternalObject) formatter.Deserialize(bytes, offset, formatterResolver, out readSize, context);
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
            return Serialize(ref bytes, offset, (TestModels.ExternalObject) value, formatterResolver, context);
        }

        object IMessagePackFormatter<object>.Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            return Deserialize(bytes, offset, formatterResolver, out readSize, context);
        }

        public void Populate(ref TestModels.ExternalObject value, byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                value = null;
                return;
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
                    value = (TestModels.ExternalObject) context.ExternalObjectsByIds[referencedObjectId];
                    return;
                }

                value = null;
                return;
            }

            if (writtedTypeId == (int) global::MessagePack.ModelSerialization.ReservedTypes.Reference)
            {
                var referencedObjectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                offset += readSize;
                readSize = offset - startOffset;
                
                if (context.DeserializedObjects.ContainsKey(referencedObjectId))
                {
                    value = (TestModels.ExternalObject) context.DeserializedObjects[referencedObjectId];
                    return;
                }
                value = null;
                return;
            }

            var actualType = TypeRegistry.Types[writtedTypeId];

            if (writtedTypeId != TypeId)
            {
                var formatter = GetFormatterForActualType(actualType, formatterResolver);
                var formatterWithPopulate = (IMessagePackUntypedFormatterWithPopulate) formatter;

                var valueObject = (object)value;
                offset = startOffset;
                formatterWithPopulate.Populate(ref valueObject, bytes, offset, formatterResolver, out readSize, context);
                return;
            }

            if (actualType != value.GetType() && actualType.IsSubclassOf(value.GetType()))
            {
                var formatter = GetFormatterForActualType(actualType, formatterResolver);
                offset = startOffset;
                value = (TestModels.ExternalObject) formatter.Deserialize(bytes, offset, formatterResolver, out readSize, context);
                return;
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

            context.DeserializedObjects[objectId] = value;

            readSize = offset - startOffset;
        }

        IMessagePackUntypedFormatter GetFormatterForActualType(Type actualType, IFormatterResolver formatterResolver)
        {
            if (!(formatterResolver is IUntypedFormatterResolver))
            {
                throw new Exception("In order to populate derived types resolver should implement IUntypedFormatterResolver");
            }

            var untypedFormatterResolver = (IUntypedFormatterResolver) formatterResolver;
            return untypedFormatterResolver.GetFormatter(actualType);
        }

        void IMessagePackFormatterWithPopulate<object>.Populate(ref object value, byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            var typedValue = (TestModels.ExternalObject) value;
            Populate(ref typedValue, bytes, offset, formatterResolver, out readSize, context);
        }
    }


    public sealed class PetFormatter : global::MessagePack.Formatters.IMessagePackFormatterWithPopulate<TestModels.Pet>, IMessagePackUntypedFormatterWithPopulate, IMessagePackUntypedFormatter
    {
        private const int TypeId = 65;

        private global::MessagePack.ModelCreation.IModelFactory _modelFactory;

        public PetFormatter(global::MessagePack.ModelCreation.IModelFactory modelFactory)
        {
            this._modelFactory = modelFactory;
        }

        public int Serialize(ref byte[] bytes, int offset, TestModels.Pet value, global::MessagePack.IFormatterResolver formatterResolver, SerializationContext context)
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

            if (value.GetType() != typeof(TestModels.Pet))
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
            offset += formatterResolver.GetFormatterWithVerify<TestModels.ExternalObject>().Serialize(ref bytes, offset, value.ExternalObject, formatterResolver, context);
            return offset - startOffset;
        }

        public TestModels.Pet Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
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
                    return (TestModels.Pet) context.ExternalObjectsByIds[referencedObjectId];
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
                    return (TestModels.Pet) context.DeserializedObjects[referencedObjectId];
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
                return (TestModels.Pet) formatter.Deserialize(bytes, offset, formatterResolver, out readSize, context);
            }

            var objectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            offset += readSize;

            var __Name__ = default(System.String);
            var __Power__ = default(System.Single);
            var __ExternalObject__ = default(TestModels.ExternalObject);

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
                        __ExternalObject__ = formatterResolver.GetFormatterWithVerify<TestModels.ExternalObject>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                offset += readSize;
            }

            readSize = offset - startOffset;


            var ____result = _modelFactory.CreateModel<TestModels.Pet>(); //new TestModels.Pet();
            ____result.Name = __Name__;
            ____result.Power = __Power__;
            ____result.ExternalObject = __ExternalObject__;

            context.DeserializedObjects[objectId] = ____result;

            return ____result;

        }

        public int Serialize(ref byte[] bytes, int offset, object value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            return Serialize(ref bytes, offset, (TestModels.Pet) value, formatterResolver, context);
        }

        object IMessagePackFormatter<object>.Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            return Deserialize(bytes, offset, formatterResolver, out readSize, context);
        }

        public void Populate(ref TestModels.Pet value, byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                value = null;
                return;
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
                    value = (TestModels.Pet) context.ExternalObjectsByIds[referencedObjectId];
                    return;
                }

                value = null;
                return;
            }

            if (writtedTypeId == (int) global::MessagePack.ModelSerialization.ReservedTypes.Reference)
            {
                var referencedObjectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                offset += readSize;
                readSize = offset - startOffset;
                
                if (context.DeserializedObjects.ContainsKey(referencedObjectId))
                {
                    value = (TestModels.Pet) context.DeserializedObjects[referencedObjectId];
                    return;
                }
                value = null;
                return;
            }

            var actualType = TypeRegistry.Types[writtedTypeId];

            if (writtedTypeId != TypeId)
            {
                var formatter = GetFormatterForActualType(actualType, formatterResolver);
                var formatterWithPopulate = (IMessagePackUntypedFormatterWithPopulate) formatter;

                var valueObject = (object)value;
                offset = startOffset;
                formatterWithPopulate.Populate(ref valueObject, bytes, offset, formatterResolver, out readSize, context);
                return;
            }

            if (actualType != value.GetType() && actualType.IsSubclassOf(value.GetType()))
            {
                var formatter = GetFormatterForActualType(actualType, formatterResolver);
                offset = startOffset;
                value = (TestModels.Pet) formatter.Deserialize(bytes, offset, formatterResolver, out readSize, context);
                return;
            }

            var objectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            offset += readSize;

            for (int i = 0; i < length - 2; i++)
            {
                var key = i;

                switch (key)
                {
                    case 0:
                        value.Name = formatterResolver.GetFormatterWithVerify<System.String>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        break;
                    case 1:
                        value.Power = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
                        break;
                    case 2:
                        var formatterExternalObject = formatterResolver.GetFormatterWithVerify<TestModels.ExternalObject>();
                        if (value.ExternalObject != null && formatterExternalObject is IMessagePackFormatterWithPopulate<TestModels.ExternalObject> formatterWithPopulateExternalObject)
                        {
                            var __ExternalObject__ = value.ExternalObject;
                            formatterWithPopulateExternalObject.Populate(ref __ExternalObject__, bytes, offset, formatterResolver, out readSize, context);
                            if (__ExternalObject__ != value.ExternalObject) {
                                value.ExternalObject = __ExternalObject__;
                            }
                        }
                        else
                        {
                            value.ExternalObject = formatterResolver.GetFormatterWithVerify<TestModels.ExternalObject>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        }
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                offset += readSize;
            }

            context.DeserializedObjects[objectId] = value;

            readSize = offset - startOffset;
        }

        IMessagePackUntypedFormatter GetFormatterForActualType(Type actualType, IFormatterResolver formatterResolver)
        {
            if (!(formatterResolver is IUntypedFormatterResolver))
            {
                throw new Exception("In order to populate derived types resolver should implement IUntypedFormatterResolver");
            }

            var untypedFormatterResolver = (IUntypedFormatterResolver) formatterResolver;
            return untypedFormatterResolver.GetFormatter(actualType);
        }

        void IMessagePackFormatterWithPopulate<object>.Populate(ref object value, byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            var typedValue = (TestModels.Pet) value;
            Populate(ref typedValue, bytes, offset, formatterResolver, out readSize, context);
        }
    }


    public sealed class PersonFormatter : global::MessagePack.Formatters.IMessagePackFormatterWithPopulate<TestModels.Person>, IMessagePackUntypedFormatterWithPopulate, IMessagePackUntypedFormatter
    {
        private const int TypeId = 64;

        private global::MessagePack.ModelCreation.IModelFactory _modelFactory;

        public PersonFormatter(global::MessagePack.ModelCreation.IModelFactory modelFactory)
        {
            this._modelFactory = modelFactory;
        }

        public int Serialize(ref byte[] bytes, int offset, TestModels.Person value, global::MessagePack.IFormatterResolver formatterResolver, SerializationContext context)
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

            if (value.GetType() != typeof(TestModels.Person))
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


            offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 13);

            offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, TypeId);
            offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, objectId);

            offset += formatterResolver.GetFormatterWithVerify<System.String>().Serialize(ref bytes, offset, value.Name, formatterResolver, context);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Age);
            offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.Height);
            offset += formatterResolver.GetFormatterWithVerify<System.Collections.Generic.List<TestModels.Pet>>().Serialize(ref bytes, offset, value.Pets, formatterResolver, context);
            offset += formatterResolver.GetFormatterWithVerify<System.Int32[]>().Serialize(ref bytes, offset, value.Numbers, formatterResolver, context);
            offset += formatterResolver.GetFormatterWithVerify<TestModels.Pet>().Serialize(ref bytes, offset, value.FavoritePet, formatterResolver, context);
            offset += formatterResolver.GetFormatterWithVerify<TestModels.ExternalObject>().Serialize(ref bytes, offset, value.ExternalObject, formatterResolver, context);
            offset += formatterResolver.GetFormatterWithVerify<System.Collections.Generic.IList<TestModels.Person>>().Serialize(ref bytes, offset, value.Dudes, formatterResolver, context);
            offset += formatterResolver.GetFormatterWithVerify<System.Collections.Generic.Dictionary<System.String, TestModels.Pet>>().Serialize(ref bytes, offset, value.LabeledPets, formatterResolver, context);
            offset += global::MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.NumberField);
            return offset - startOffset;
        }

        public TestModels.Person Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
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
                    return (TestModels.Person) context.ExternalObjectsByIds[referencedObjectId];
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
                    return (TestModels.Person) context.DeserializedObjects[referencedObjectId];
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
                return (TestModels.Person) formatter.Deserialize(bytes, offset, formatterResolver, out readSize, context);
            }

            var objectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            offset += readSize;

            var __Name__ = default(System.String);
            var __Age__ = default(System.Int32);
            var __Height__ = default(System.Single);
            var __Pets__ = default(System.Collections.Generic.List<TestModels.Pet>);
            var __Numbers__ = default(System.Int32[]);
            var __FavoritePet__ = default(TestModels.Pet);
            var __ExternalObject__ = default(TestModels.ExternalObject);
            var __Dudes__ = default(System.Collections.Generic.IList<TestModels.Person>);
            var __LabeledPets__ = default(System.Collections.Generic.Dictionary<System.String, TestModels.Pet>);
            var __NumberField__ = default(System.Int32);

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
                        __Pets__ = formatterResolver.GetFormatterWithVerify<System.Collections.Generic.List<TestModels.Pet>>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        break;
                    case 4:
                        __Numbers__ = formatterResolver.GetFormatterWithVerify<System.Int32[]>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        break;
                    case 5:
                        __FavoritePet__ = formatterResolver.GetFormatterWithVerify<TestModels.Pet>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        break;
                    case 6:
                        __ExternalObject__ = formatterResolver.GetFormatterWithVerify<TestModels.ExternalObject>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        break;
                    case 7:
                        __Dudes__ = formatterResolver.GetFormatterWithVerify<System.Collections.Generic.IList<TestModels.Person>>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        break;
                    case 8:
                        __LabeledPets__ = formatterResolver.GetFormatterWithVerify<System.Collections.Generic.Dictionary<System.String, TestModels.Pet>>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        break;
                    case 10:
                        __NumberField__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                offset += readSize;
            }

            readSize = offset - startOffset;


            var ____result = _modelFactory.CreateModel<TestModels.Person>(); //new TestModels.Person();
            ____result.Name = __Name__;
            ____result.Age = __Age__;
            ____result.Height = __Height__;
            ____result.Pets = __Pets__;
            ____result.Numbers = __Numbers__;
            ____result.FavoritePet = __FavoritePet__;
            ____result.ExternalObject = __ExternalObject__;
            ____result.Dudes = __Dudes__;
            ____result.LabeledPets = __LabeledPets__;
            ____result.NumberField = __NumberField__;

            context.DeserializedObjects[objectId] = ____result;

            return ____result;

        }

        public int Serialize(ref byte[] bytes, int offset, object value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            return Serialize(ref bytes, offset, (TestModels.Person) value, formatterResolver, context);
        }

        object IMessagePackFormatter<object>.Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            return Deserialize(bytes, offset, formatterResolver, out readSize, context);
        }

        public void Populate(ref TestModels.Person value, byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                value = null;
                return;
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
                    value = (TestModels.Person) context.ExternalObjectsByIds[referencedObjectId];
                    return;
                }

                value = null;
                return;
            }

            if (writtedTypeId == (int) global::MessagePack.ModelSerialization.ReservedTypes.Reference)
            {
                var referencedObjectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                offset += readSize;
                readSize = offset - startOffset;
                
                if (context.DeserializedObjects.ContainsKey(referencedObjectId))
                {
                    value = (TestModels.Person) context.DeserializedObjects[referencedObjectId];
                    return;
                }
                value = null;
                return;
            }

            var actualType = TypeRegistry.Types[writtedTypeId];

            if (writtedTypeId != TypeId)
            {
                var formatter = GetFormatterForActualType(actualType, formatterResolver);
                var formatterWithPopulate = (IMessagePackUntypedFormatterWithPopulate) formatter;

                var valueObject = (object)value;
                offset = startOffset;
                formatterWithPopulate.Populate(ref valueObject, bytes, offset, formatterResolver, out readSize, context);
                return;
            }

            if (actualType != value.GetType() && actualType.IsSubclassOf(value.GetType()))
            {
                var formatter = GetFormatterForActualType(actualType, formatterResolver);
                offset = startOffset;
                value = (TestModels.Person) formatter.Deserialize(bytes, offset, formatterResolver, out readSize, context);
                return;
            }

            var objectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            offset += readSize;

            for (int i = 0; i < length - 2; i++)
            {
                var key = i;

                switch (key)
                {
                    case 0:
                        value.Name = formatterResolver.GetFormatterWithVerify<System.String>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        break;
                    case 1:
                        value.Age = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 2:
                        value.Height = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
                        break;
                    case 3:
                        var formatterPets = formatterResolver.GetFormatterWithVerify<System.Collections.Generic.List<TestModels.Pet>>();
                        if (value.Pets != null && formatterPets is IMessagePackFormatterWithPopulate<System.Collections.Generic.List<TestModels.Pet>> formatterWithPopulatePets)
                        {
                            var __Pets__ = value.Pets;
                            formatterWithPopulatePets.Populate(ref __Pets__, bytes, offset, formatterResolver, out readSize, context);
                            if (__Pets__ != value.Pets) {
                                value.Pets = __Pets__;
                            }
                        }
                        else
                        {
                            value.Pets = formatterResolver.GetFormatterWithVerify<System.Collections.Generic.List<TestModels.Pet>>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        }
                        break;
                    case 4:
                        value.Numbers = formatterResolver.GetFormatterWithVerify<System.Int32[]>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        break;
                    case 5:
                        var formatterFavoritePet = formatterResolver.GetFormatterWithVerify<TestModels.Pet>();
                        if (value.FavoritePet != null && formatterFavoritePet is IMessagePackFormatterWithPopulate<TestModels.Pet> formatterWithPopulateFavoritePet)
                        {
                            var __FavoritePet__ = value.FavoritePet;
                            formatterWithPopulateFavoritePet.Populate(ref __FavoritePet__, bytes, offset, formatterResolver, out readSize, context);
                            if (__FavoritePet__ != value.FavoritePet) {
                                value.FavoritePet = __FavoritePet__;
                            }
                        }
                        else
                        {
                            value.FavoritePet = formatterResolver.GetFormatterWithVerify<TestModels.Pet>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        }
                        break;
                    case 6:
                        var formatterExternalObject = formatterResolver.GetFormatterWithVerify<TestModels.ExternalObject>();
                        if (value.ExternalObject != null && formatterExternalObject is IMessagePackFormatterWithPopulate<TestModels.ExternalObject> formatterWithPopulateExternalObject)
                        {
                            var __ExternalObject__ = value.ExternalObject;
                            formatterWithPopulateExternalObject.Populate(ref __ExternalObject__, bytes, offset, formatterResolver, out readSize, context);
                            if (__ExternalObject__ != value.ExternalObject) {
                                value.ExternalObject = __ExternalObject__;
                            }
                        }
                        else
                        {
                            value.ExternalObject = formatterResolver.GetFormatterWithVerify<TestModels.ExternalObject>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        }
                        break;
                    case 7:
                        var formatterDudes = formatterResolver.GetFormatterWithVerify<System.Collections.Generic.IList<TestModels.Person>>();
                        if (value.Dudes != null && formatterDudes is IMessagePackFormatterWithPopulate<System.Collections.Generic.IList<TestModels.Person>> formatterWithPopulateDudes)
                        {
                            var __Dudes__ = value.Dudes;
                            formatterWithPopulateDudes.Populate(ref __Dudes__, bytes, offset, formatterResolver, out readSize, context);
                            if (__Dudes__ != value.Dudes) {
                                value.Dudes = __Dudes__;
                            }
                        }
                        else
                        {
                            value.Dudes = formatterResolver.GetFormatterWithVerify<System.Collections.Generic.IList<TestModels.Person>>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        }
                        break;
                    case 8:
                        var formatterLabeledPets = formatterResolver.GetFormatterWithVerify<System.Collections.Generic.Dictionary<System.String, TestModels.Pet>>();
                        if (value.LabeledPets != null && formatterLabeledPets is IMessagePackFormatterWithPopulate<System.Collections.Generic.Dictionary<System.String, TestModels.Pet>> formatterWithPopulateLabeledPets)
                        {
                            var __LabeledPets__ = value.LabeledPets;
                            formatterWithPopulateLabeledPets.Populate(ref __LabeledPets__, bytes, offset, formatterResolver, out readSize, context);
                            if (__LabeledPets__ != value.LabeledPets) {
                                value.LabeledPets = __LabeledPets__;
                            }
                        }
                        else
                        {
                            value.LabeledPets = formatterResolver.GetFormatterWithVerify<System.Collections.Generic.Dictionary<System.String, TestModels.Pet>>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        }
                        break;
                    case 10:
                        value.NumberField = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                offset += readSize;
            }

            context.DeserializedObjects[objectId] = value;

            readSize = offset - startOffset;
        }

        IMessagePackUntypedFormatter GetFormatterForActualType(Type actualType, IFormatterResolver formatterResolver)
        {
            if (!(formatterResolver is IUntypedFormatterResolver))
            {
                throw new Exception("In order to populate derived types resolver should implement IUntypedFormatterResolver");
            }

            var untypedFormatterResolver = (IUntypedFormatterResolver) formatterResolver;
            return untypedFormatterResolver.GetFormatter(actualType);
        }

        void IMessagePackFormatterWithPopulate<object>.Populate(ref object value, byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            var typedValue = (TestModels.Person) value;
            Populate(ref typedValue, bytes, offset, formatterResolver, out readSize, context);
        }
    }


    public sealed class SuperPetFormatter : global::MessagePack.Formatters.IMessagePackFormatterWithPopulate<TestModels.SuperPet>, IMessagePackUntypedFormatterWithPopulate, IMessagePackUntypedFormatter
    {
        private const int TypeId = 66;

        private global::MessagePack.ModelCreation.IModelFactory _modelFactory;

        public SuperPetFormatter(global::MessagePack.ModelCreation.IModelFactory modelFactory)
        {
            this._modelFactory = modelFactory;
        }

        public int Serialize(ref byte[] bytes, int offset, TestModels.SuperPet value, global::MessagePack.IFormatterResolver formatterResolver, SerializationContext context)
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

            if (value.GetType() != typeof(TestModels.SuperPet))
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
            offset += formatterResolver.GetFormatterWithVerify<TestModels.ExternalObject>().Serialize(ref bytes, offset, value.ExternalObject, formatterResolver, context);
            return offset - startOffset;
        }

        public TestModels.SuperPet Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
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
                    return (TestModels.SuperPet) context.ExternalObjectsByIds[referencedObjectId];
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
                    return (TestModels.SuperPet) context.DeserializedObjects[referencedObjectId];
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
                return (TestModels.SuperPet) formatter.Deserialize(bytes, offset, formatterResolver, out readSize, context);
            }

            var objectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            offset += readSize;

            var __Kind__ = default(System.Int32);
            var __Name__ = default(System.String);
            var __Power__ = default(System.Single);
            var __ExternalObject__ = default(TestModels.ExternalObject);

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
                        __ExternalObject__ = formatterResolver.GetFormatterWithVerify<TestModels.ExternalObject>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                offset += readSize;
            }

            readSize = offset - startOffset;


            var ____result = _modelFactory.CreateModel<TestModels.SuperPet>(); //new TestModels.SuperPet();
            ____result.Kind = __Kind__;
            ____result.Name = __Name__;
            ____result.Power = __Power__;
            ____result.ExternalObject = __ExternalObject__;

            context.DeserializedObjects[objectId] = ____result;

            return ____result;

        }

        public int Serialize(ref byte[] bytes, int offset, object value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            return Serialize(ref bytes, offset, (TestModels.SuperPet) value, formatterResolver, context);
        }

        object IMessagePackFormatter<object>.Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            return Deserialize(bytes, offset, formatterResolver, out readSize, context);
        }

        public void Populate(ref TestModels.SuperPet value, byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                value = null;
                return;
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
                    value = (TestModels.SuperPet) context.ExternalObjectsByIds[referencedObjectId];
                    return;
                }

                value = null;
                return;
            }

            if (writtedTypeId == (int) global::MessagePack.ModelSerialization.ReservedTypes.Reference)
            {
                var referencedObjectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                offset += readSize;
                readSize = offset - startOffset;
                
                if (context.DeserializedObjects.ContainsKey(referencedObjectId))
                {
                    value = (TestModels.SuperPet) context.DeserializedObjects[referencedObjectId];
                    return;
                }
                value = null;
                return;
            }

            var actualType = TypeRegistry.Types[writtedTypeId];

            if (writtedTypeId != TypeId)
            {
                var formatter = GetFormatterForActualType(actualType, formatterResolver);
                var formatterWithPopulate = (IMessagePackUntypedFormatterWithPopulate) formatter;

                var valueObject = (object)value;
                offset = startOffset;
                formatterWithPopulate.Populate(ref valueObject, bytes, offset, formatterResolver, out readSize, context);
                return;
            }

            if (actualType != value.GetType() && actualType.IsSubclassOf(value.GetType()))
            {
                var formatter = GetFormatterForActualType(actualType, formatterResolver);
                offset = startOffset;
                value = (TestModels.SuperPet) formatter.Deserialize(bytes, offset, formatterResolver, out readSize, context);
                return;
            }

            var objectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            offset += readSize;

            for (int i = 0; i < length - 2; i++)
            {
                var key = i;

                switch (key)
                {
                    case 0:
                        value.Kind = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 1:
                        value.Name = formatterResolver.GetFormatterWithVerify<System.String>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        break;
                    case 2:
                        value.Power = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
                        break;
                    case 3:
                        var formatterExternalObject = formatterResolver.GetFormatterWithVerify<TestModels.ExternalObject>();
                        if (value.ExternalObject != null && formatterExternalObject is IMessagePackFormatterWithPopulate<TestModels.ExternalObject> formatterWithPopulateExternalObject)
                        {
                            var __ExternalObject__ = value.ExternalObject;
                            formatterWithPopulateExternalObject.Populate(ref __ExternalObject__, bytes, offset, formatterResolver, out readSize, context);
                            if (__ExternalObject__ != value.ExternalObject) {
                                value.ExternalObject = __ExternalObject__;
                            }
                        }
                        else
                        {
                            value.ExternalObject = formatterResolver.GetFormatterWithVerify<TestModels.ExternalObject>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        }
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                offset += readSize;
            }

            context.DeserializedObjects[objectId] = value;

            readSize = offset - startOffset;
        }

        IMessagePackUntypedFormatter GetFormatterForActualType(Type actualType, IFormatterResolver formatterResolver)
        {
            if (!(formatterResolver is IUntypedFormatterResolver))
            {
                throw new Exception("In order to populate derived types resolver should implement IUntypedFormatterResolver");
            }

            var untypedFormatterResolver = (IUntypedFormatterResolver) formatterResolver;
            return untypedFormatterResolver.GetFormatter(actualType);
        }

        void IMessagePackFormatterWithPopulate<object>.Populate(ref object value, byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            var typedValue = (TestModels.SuperPet) value;
            Populate(ref typedValue, bytes, offset, formatterResolver, out readSize, context);
        }
    }


    public sealed class UltimatePetFormatter : global::MessagePack.Formatters.IMessagePackFormatterWithPopulate<TestModels.UltimatePet>, IMessagePackUntypedFormatterWithPopulate, IMessagePackUntypedFormatter
    {
        private const int TypeId = 67;

        private global::MessagePack.ModelCreation.IModelFactory _modelFactory;

        public UltimatePetFormatter(global::MessagePack.ModelCreation.IModelFactory modelFactory)
        {
            this._modelFactory = modelFactory;
        }

        public int Serialize(ref byte[] bytes, int offset, TestModels.UltimatePet value, global::MessagePack.IFormatterResolver formatterResolver, SerializationContext context)
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

            if (value.GetType() != typeof(TestModels.UltimatePet))
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


            offset += global::MessagePack.MessagePackBinary.WriteFixedArrayHeaderUnsafe(ref bytes, offset, 7);

            offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, TypeId);
            offset += global::MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, objectId);

            offset += formatterResolver.GetFormatterWithVerify<System.String>().Serialize(ref bytes, offset, value.UberPowerName, formatterResolver, context);
            offset += MessagePackBinary.WriteInt32(ref bytes, offset, value.Kind);
            offset += formatterResolver.GetFormatterWithVerify<System.String>().Serialize(ref bytes, offset, value.Name, formatterResolver, context);
            offset += MessagePackBinary.WriteSingle(ref bytes, offset, value.Power);
            offset += formatterResolver.GetFormatterWithVerify<TestModels.ExternalObject>().Serialize(ref bytes, offset, value.ExternalObject, formatterResolver, context);
            return offset - startOffset;
        }

        public TestModels.UltimatePet Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
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
                    return (TestModels.UltimatePet) context.ExternalObjectsByIds[referencedObjectId];
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
                    return (TestModels.UltimatePet) context.DeserializedObjects[referencedObjectId];
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
                return (TestModels.UltimatePet) formatter.Deserialize(bytes, offset, formatterResolver, out readSize, context);
            }

            var objectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            offset += readSize;

            var __UberPowerName__ = default(System.String);
            var __Kind__ = default(System.Int32);
            var __Name__ = default(System.String);
            var __Power__ = default(System.Single);
            var __ExternalObject__ = default(TestModels.ExternalObject);

            for (int i = 0; i < length - 2; i++)
            {
                var key = i;

                switch (key)
                {
                    case 0:
                        __UberPowerName__ = formatterResolver.GetFormatterWithVerify<System.String>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        break;
                    case 1:
                        __Kind__ = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 2:
                        __Name__ = formatterResolver.GetFormatterWithVerify<System.String>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        break;
                    case 3:
                        __Power__ = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
                        break;
                    case 4:
                        __ExternalObject__ = formatterResolver.GetFormatterWithVerify<TestModels.ExternalObject>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                offset += readSize;
            }

            readSize = offset - startOffset;


            var ____result = _modelFactory.CreateModel<TestModels.UltimatePet>(); //new TestModels.UltimatePet();
            ____result.UberPowerName = __UberPowerName__;
            ____result.Kind = __Kind__;
            ____result.Name = __Name__;
            ____result.Power = __Power__;
            ____result.ExternalObject = __ExternalObject__;

            context.DeserializedObjects[objectId] = ____result;

            return ____result;

        }

        public int Serialize(ref byte[] bytes, int offset, object value, IFormatterResolver formatterResolver, SerializationContext context)
        {
            return Serialize(ref bytes, offset, (TestModels.UltimatePet) value, formatterResolver, context);
        }

        object IMessagePackFormatter<object>.Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            return Deserialize(bytes, offset, formatterResolver, out readSize, context);
        }

        public void Populate(ref TestModels.UltimatePet value, byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            if (global::MessagePack.MessagePackBinary.IsNil(bytes, offset))
            {
                readSize = 1;
                value = null;
                return;
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
                    value = (TestModels.UltimatePet) context.ExternalObjectsByIds[referencedObjectId];
                    return;
                }

                value = null;
                return;
            }

            if (writtedTypeId == (int) global::MessagePack.ModelSerialization.ReservedTypes.Reference)
            {
                var referencedObjectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                offset += readSize;
                readSize = offset - startOffset;
                
                if (context.DeserializedObjects.ContainsKey(referencedObjectId))
                {
                    value = (TestModels.UltimatePet) context.DeserializedObjects[referencedObjectId];
                    return;
                }
                value = null;
                return;
            }

            var actualType = TypeRegistry.Types[writtedTypeId];

            if (writtedTypeId != TypeId)
            {
                var formatter = GetFormatterForActualType(actualType, formatterResolver);
                var formatterWithPopulate = (IMessagePackUntypedFormatterWithPopulate) formatter;

                var valueObject = (object)value;
                offset = startOffset;
                formatterWithPopulate.Populate(ref valueObject, bytes, offset, formatterResolver, out readSize, context);
                return;
            }

            if (actualType != value.GetType() && actualType.IsSubclassOf(value.GetType()))
            {
                var formatter = GetFormatterForActualType(actualType, formatterResolver);
                offset = startOffset;
                value = (TestModels.UltimatePet) formatter.Deserialize(bytes, offset, formatterResolver, out readSize, context);
                return;
            }

            var objectId = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            offset += readSize;

            for (int i = 0; i < length - 2; i++)
            {
                var key = i;

                switch (key)
                {
                    case 0:
                        value.UberPowerName = formatterResolver.GetFormatterWithVerify<System.String>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        break;
                    case 1:
                        value.Kind = MessagePackBinary.ReadInt32(bytes, offset, out readSize);
                        break;
                    case 2:
                        value.Name = formatterResolver.GetFormatterWithVerify<System.String>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        break;
                    case 3:
                        value.Power = MessagePackBinary.ReadSingle(bytes, offset, out readSize);
                        break;
                    case 4:
                        var formatterExternalObject = formatterResolver.GetFormatterWithVerify<TestModels.ExternalObject>();
                        if (value.ExternalObject != null && formatterExternalObject is IMessagePackFormatterWithPopulate<TestModels.ExternalObject> formatterWithPopulateExternalObject)
                        {
                            var __ExternalObject__ = value.ExternalObject;
                            formatterWithPopulateExternalObject.Populate(ref __ExternalObject__, bytes, offset, formatterResolver, out readSize, context);
                            if (__ExternalObject__ != value.ExternalObject) {
                                value.ExternalObject = __ExternalObject__;
                            }
                        }
                        else
                        {
                            value.ExternalObject = formatterResolver.GetFormatterWithVerify<TestModels.ExternalObject>().Deserialize(bytes, offset, formatterResolver, out readSize, context);
                        }
                        break;
                    default:
                        readSize = global::MessagePack.MessagePackBinary.ReadNextBlock(bytes, offset);
                        break;
                }
                offset += readSize;
            }

            context.DeserializedObjects[objectId] = value;

            readSize = offset - startOffset;
        }

        IMessagePackUntypedFormatter GetFormatterForActualType(Type actualType, IFormatterResolver formatterResolver)
        {
            if (!(formatterResolver is IUntypedFormatterResolver))
            {
                throw new Exception("In order to populate derived types resolver should implement IUntypedFormatterResolver");
            }

            var untypedFormatterResolver = (IUntypedFormatterResolver) formatterResolver;
            return untypedFormatterResolver.GetFormatter(actualType);
        }

        void IMessagePackFormatterWithPopulate<object>.Populate(ref object value, byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize, DeserializationContext context)
        {
            var typedValue = (TestModels.UltimatePet) value;
            Populate(ref typedValue, bytes, offset, formatterResolver, out readSize, context);
        }
    }

}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612
