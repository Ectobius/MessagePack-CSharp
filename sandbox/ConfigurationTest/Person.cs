using System.Collections.Generic;

namespace ConfigurationTest
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public float Height { get; set; }
        
        public List<Pet> Pets { get; set; }
        public int[] Numbers { get; set; }

        public override string ToString()
        {
            return $"{Name}:{Age}";
        }
    }
}
