using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationTest
{
    public class SuperPet : Pet
    {
        public int Kind { get; set; }

        public override string ToString()
        {
            return $"{nameof(SuperPet)}: {Name}, {Power}, {Kind}";
        }
    }
}
