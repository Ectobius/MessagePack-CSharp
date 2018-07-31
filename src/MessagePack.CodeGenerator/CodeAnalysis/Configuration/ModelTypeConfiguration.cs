using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MessagePack.CodeGenerator.CodeAnalysis.Configuration
{
    public class ModelTypeConfiguration
    {
        public int NextValidKey { get; set; } = 0;
        public List<MemberConfiguration> Members { get; set; } = new List<MemberConfiguration>();
        public int TypeId { get; set; }

        [JsonIgnore]
        public Type Type { get; set; }

        public int GetNextKey()
        {
            return NextValidKey++;
        }
    }
}
