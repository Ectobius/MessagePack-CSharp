using System;
using System.Collections.Generic;
using System.Text;

namespace MessagePack.Formatters
{
    public class DeserializationContext : MetaContext
    {
        public Dictionary<int, object> DeserializedObjects  { get; } = new Dictionary<int, object>(1000);
        public Dictionary<int, object> ExternalObjectsByIds { get; set; }

        public object ExtraData { get; set; }

        public void Reset()
        {
            DeserializedObjects.Clear();
        }

        public DeserializationContext(IModelFactory modelFactory, bool onlyKnownRefs) : base(modelFactory, onlyKnownRefs) { }
    }
}