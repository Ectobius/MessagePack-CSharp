using System;
using System.Collections.Generic;
using System.Text;

namespace MessagePack.Formatters
{
    public class SerializationContext
    {
        private int _nextValidId = 0;

        public Dictionary<object, int> SerializedObjects { get; } = new Dictionary<object, int>();

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
    }
}
