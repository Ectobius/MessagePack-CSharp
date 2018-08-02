using System;
using System.Collections.Generic;
using System.Text;

namespace MessagePack
{
    public class DeserializationOptions
    {
        public Dictionary<int, object> ExternalObjectsByIds { get; set; }
    }
}
