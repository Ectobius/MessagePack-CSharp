using System;
using System.Collections.Generic;
using System.Linq;
using MessagePack;

namespace BenchmarkOriginal
{
    public class Benchmark
    {
        private Person _person;
        private byte[] _serialized;

        public Benchmark()
        {
            CreateModel(10);
        }

        public void SerializeModel()
        {
            var bytes = MessagePackSerializer.Serialize(_person);
        }

        public void DeserializeModel()
        {
            var person = MessagePackSerializer.Deserialize<Person>(_serialized);
        }

        public void CreateModel(int petsCount)
        {
            var tima = new Pet { Name = "Tima", Power = 7.9f };

            var person = new Person
            {
                Name = "Alex",
                Age = 28,
                Pets = CreatePets(petsCount),
                FavoritePet = new Pet { Name = "Super Lucky", Power = 170.9f },
                Numbers = new[] { 3, 9, 17, 32 },
                Dudes = new List<Person>
                {
                    new Person { Name = "John", Age = 31 },
                    new Person { Name = "Alice", Age = 21 }
                },
                LabeledPets = new Dictionary<string, Pet>
                {
                    { "green", tima },
                    { "fieldy", new Pet { Name = "Mike", Power =  9.1f } }
                }
            };

            _person = person;

            _serialized = MessagePackSerializer.Serialize(_person);
        }

        private List<Pet> CreatePets(int count)
        {
            return Enumerable.Range(0, count).Select(index => new Pet
            {
                Name = GenerateName(10 + count % 10),
                Power = index
            }).ToList();
        }

        private string GenerateName(int length)
        {
            return string.Join("", Enumerable.Range(0, length).Select(i => 'a'));
        } 
    }
}
