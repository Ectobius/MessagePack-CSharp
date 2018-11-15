using System;
using System.Collections.Generic;
using System.Text;

namespace MessagePack.Formatters
{
    public class SerializationContext : MetaContext
    {
        private int _nextValidId         = 0;
        private int _nextValidExternalId = 0;

        public Dictionary<object, int> SerializedObjects    { get; } = new Dictionary<object, int>(1000);
        public Dictionary<int, object> ExternalObjectsByIds { get; set; }
        public Dictionary<object, int> ExternalObjects      { get; } = new Dictionary<object, int>();

        public object ExtraData { get; set; }

        public int GetNextValidId()
        {
            return _nextValidId++;
        }

        public int GetNextValidExternalId()
        {
            return _nextValidExternalId++;
        }

        public int PutToSerialized(object model)
        {
            int id = GetNextValidId();
            SerializedObjects[model] = id;
            return id;
        }

        public int PutToExternalObjects(object obj)
        {
            if(ExternalObjects.ContainsKey(obj))
            {
                return ExternalObjects[obj];
            }

            int id = GetNextValidExternalId();
            ExternalObjectsByIds[id] = obj;
            ExternalObjects[obj] = id;
            return id;
        }

        public void Reset()
        {
            SerializedObjects.Clear();
            ExternalObjects.Clear();
            _nextValidId = 0;
            _nextValidExternalId = 0;
        }

        public SerializationContext(IModelFactory modelFactory, bool onlyKnownRefs) :
            base(modelFactory, onlyKnownRefs) { }
    }


    public class MetaContext
    {
        public readonly Dictionary<int, object> ExternalObjectsByIds = new Dictionary<int, object>();
        public          bool                    OnlyKnownRefs;
        public readonly IModelFactory           ModelFactory;

        public MetaContext(IModelFactory modelFactory, bool onlyKnownRefs)
        {
            ModelFactory = modelFactory;
            OnlyKnownRefs = onlyKnownRefs;
        }
    }

    public interface IModelFactory
    {
        MetaContext MetaContext { get; set; }
        T           CreateModel<T>();
        T           GetOrCreateById<T>(int nodeId, int netId) where T : class;
        T           GetById<T>(int         nodeId, int netId) where T : class;
    }
}