using System.Collections.Generic;

namespace MessagePack.Formatters
{
    public class SerializationContext : MetaContext
    {
        private int _nextValidId = 0;

        public Dictionary<object, int> SerializedObjects { get; } = new Dictionary<object, int>(1000);

        public int GetNextValidId()
        {
            return _nextValidId++;
        }

        public int PutToSerialized(object model)
        {
            int id = GetNextValidId();
            SerializedObjects[model] = id;
            return id;
        }

        public void Reset()
        {
            SerializedObjects.Clear();
            _nextValidId = 0;
        }

        public SerializationContext(IModelFactory modelFactory, IMetaInfoFormatter metaInfoFormatter, bool onlyKnownRefs) :
            base(modelFactory, metaInfoFormatter, onlyKnownRefs) { }
    }


    public class MetaContext
    {
        public          bool          OnlyKnownRefs;
        public readonly IModelFactory ModelFactory;
        public readonly IMetaInfoFormatter MetaInfoFormatter;

        public int? CurrentObjectId { get; set; }

        public MetaContext(IModelFactory modelFactory, IMetaInfoFormatter metaInfoFormatter, bool onlyKnownRefs)
        {
            ModelFactory = modelFactory;
            OnlyKnownRefs = onlyKnownRefs;
            MetaInfoFormatter = metaInfoFormatter;
        }
    }

    public interface IModelFactory
    {
        MetaContext MetaContext { get; set; }
        T           CreateModel<T>();
        T           GetOrCreateById<T>(int nodeId, int netId);
        T           GetById<T>(int         nodeId, int netId);
    }
}