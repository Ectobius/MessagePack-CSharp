using System.Collections.Generic;

namespace TestModels
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public float Height { get; set; }
        public List<Pet> Pets { get; set; }
        public int[] Numbers { get; set; }

        public Pet FavoritePet { get; set; }

        public ExternalObject ExternalObject { get; set; }

        public IList<Person> Dudes { get; set; }

        public Dictionary<string, Pet> LabeledPets { get; set; }

        public int NumberField;

        public override string ToString()
        {
            return $"{Name}:{Age}";
        }
    }
}
