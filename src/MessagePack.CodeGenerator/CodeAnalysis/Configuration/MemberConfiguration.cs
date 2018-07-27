using System.Reflection;
using Newtonsoft.Json;

namespace MessagePack.CodeGenerator.CodeAnalysis.Configuration
{
    public class MemberConfiguration
    {
        public string Name { get; set; }
        public string TypeFullName { get; set; }
        public int Key { get; set; }

        [JsonIgnore]
        public MemberInfo MemberInfo { get; set; }
    }
}
