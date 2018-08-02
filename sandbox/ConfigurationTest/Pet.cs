using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationTest
{
    public class Pet
    {
        public string Name { get; set; }
        public float Power { get; set; }

        public ExternalObject ExternalObject { get; set; }

        public override string ToString()
        {
            return $"{nameof(Pet)}: {Name}, {Power}";
        }
    }
}
