using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationTest
{
    public class UltimatePet : SuperPet
    {
        public string UberPowerName { get; set; }

        public override string ToString()
        {
            return $"{nameof(UltimatePet)}: {Name}, {Power}, {Kind}, {UberPowerName}";
        }
    }
}
