using System;
using System.Collections.Generic;
using System.Linq;
using MessagePack;
using TestModels;

namespace BenchmarkCustomized
{
    public class BenchmarkSimpleModel
    {
        private Person _person;
        private Person _personToPopulate;
        private SerializationOptions _serializationOptions;
        private DeserializationOptions _deserializationOptions;
        private byte[] _serialized;

        public BenchmarkSimpleModel()
        {
            _serializationOptions = new SerializationOptions();

            _deserializationOptions = new DeserializationOptions();
        }

        public void IterationSetup()
        {
            _serializationOptions.ExternalObjectsByIds.Clear();

            foreach (var pet in _personToPopulate.Pets)
            {
                pet.Name = "New Name";
            }
        }

        public void SerializeModel()
        {
            var bytes = MessagePackSerializer.Serialize(_person, _serializationOptions);
        }

        public void DeserializeModel()
        {
            var person = MessagePackSerializer.Deserialize<Person>(_serialized, _deserializationOptions);
        }

        public void PopulateModel()
        {
            MessagePackSerializer.Populate(ref _personToPopulate, _serialized, _deserializationOptions);
        }

        public void Setup(int size)
        {
            _person = CreateModel(size);
            _personToPopulate = CreateModel(size);

            _serialized = MessagePackSerializer.Serialize(_person);
        }

        private Person CreateModel(int petsCount)
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

            return person;
        }

        private List<Pet> CreatePets(int count)
        {
            return Enumerable.Range(0, count).Select(index => new Pet
            {
                Name = ModelGeneration.GenerateName(10 + count % 10),
                Power = index
            }).ToList();
        }
    }
}
