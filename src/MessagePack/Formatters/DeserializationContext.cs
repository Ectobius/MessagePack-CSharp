using System;
using System.Collections.Generic;
using System.Text;

namespace MessagePack.Formatters
{
    public class DeserializationContext
    {
        public Dictionary<int, object> DeserializedObjects { get; } = new Dictionary<int, object>(1000);
        public Dictionary<int, object> ExternalObjectsByIds { get; set; }

        public object ExtraData { get; set; }
    }
}
