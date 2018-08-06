using System.Collections.Generic;
using MessagePack;

namespace BenchmarkOriginal
{
    [MessagePackObject]
    public class Person
    {
        [Key(0)]
        public string Name { get; set; }

        [Key(1)]
        public int Age { get; set; }

        [Key(2)]
        public float Height { get; set; }

        [Key(3)]
        public List<Pet> Pets { get; set; }

        [Key(4)]
        public int[] Numbers { get; set; }

        [Key(5)]
        public Pet FavoritePet { get; set; }

        [Key(6)]
        public IList<Person> Dudes { get; set; }

        [Key(7)]
        public Dictionary<string, Pet> LabeledPets { get; set; }

        public override string ToString()
        {
            return $"{Name}:{Age}";
        }
    }
}
