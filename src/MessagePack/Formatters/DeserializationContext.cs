using System.Collections.Generic;

namespace MessagePack.Formatters
{
    public class DeserializationContext : MetaContext
    {
        public Dictionary<int, object> DeserializedObjects { get; } = new Dictionary<int, object>(1000);

        public void Reset()
        {
            DeserializedObjects.Clear();
        }

        public DeserializationContext(IModelFactory modelFactory, IMetaInfoFormatter metaInfoFormatter,
                                      bool          onlyKnownRefs) : base(
            modelFactory, metaInfoFormatter, onlyKnownRefs) { }
    }
}