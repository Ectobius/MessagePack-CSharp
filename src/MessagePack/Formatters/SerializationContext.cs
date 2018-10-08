using System;
using System.Collections.Generic;
using System.Text;

namespace MessagePack.Formatters
{
    public class SerializationContext
    {
        private int _nextValidId = 0;
        private int _nextValidExternalId = 0;

        public SerializationContext()
        {
            ExternalObjectsByIds = new Dictionary<int, object>();
        }

        public SerializationContext(SerializationOptions options)
        {
            ExternalObjectsByIds = options.ExternalObjectsByIds;
            ExternalReferenceChecker = options.ExternalReferenceChecker;
        }

        public Dictionary<object, int> SerializedObjects { get; } = new Dictionary<object, int>(1000);
        public Func<object, bool> ExternalReferenceChecker { get; set; }
        public Dictionary<int, object> ExternalObjectsByIds { get; set; }
        public Dictionary<object, int> ExternalObjects { get; } = new Dictionary<object, int>();

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
            if (ExternalObjects.ContainsKey(obj))
            {
                return ExternalObjects[obj];
            }

            int id = GetNextValidExternalId();
            ExternalObjectsByIds[id] = obj;
            ExternalObjects[obj] = id;
            return id;
        }

        public bool CheckIfExternal(object obj)
        {
            if (ExternalReferenceChecker == null || ExternalObjectsByIds == null)
            {
                return false;
            }

            return ExternalReferenceChecker(obj);
        }

        public void Reset()
        {
            SerializedObjects.Clear();
            ExternalObjects.Clear();
            _nextValidId = 0;
            _nextValidExternalId = 0;
        }
    }
}
