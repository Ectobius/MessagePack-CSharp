using System;
using System.Collections.Generic;
using System.Text;

namespace MessagePack
{
    public class SerializationOptions
    {
        public Func<object, bool> ExternalReferenceChecker { get; set; }
        public Dictionary<int, object> ExternalObjectsByIds { get; } = new Dictionary<int, object>();

        public object ExtraData { get; set; }
    }
}
