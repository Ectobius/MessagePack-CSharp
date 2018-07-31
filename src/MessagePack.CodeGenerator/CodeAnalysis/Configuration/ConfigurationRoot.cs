using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagePack.CodeGenerator.CodeAnalysis.Configuration
{
    public class ConfigurationRoot
    {
        public const int MinCustomTypeId = 64;

        public Dictionary<string, ModelTypeConfiguration> ModelConfigurations { get; set; } =
            new Dictionary<string, ModelTypeConfiguration>();

        public int NextValidTypeId { get; set; } = MinCustomTypeId;

        public int GetNextTypeId()
        {
            return NextValidTypeId++;
        }
    }
}
