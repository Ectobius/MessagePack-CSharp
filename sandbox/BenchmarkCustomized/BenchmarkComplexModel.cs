using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessagePack;
using TestModels;

namespace BenchmarkCustomized
{
    public class BenchmarkComplexModel
    {
        private Person _person;
        private SerializationOptions _serializationOptions;
        private DeserializationOptions _deserializationOptions;
        private byte[] _serialized;

        public BenchmarkComplexModel()
        {
            _serializationOptions = new SerializationOptions
            {
                ExternalReferenceChecker = (obj) => obj.GetType() == typeof(ExternalObject)
            };

            _deserializationOptions = new DeserializationOptions();
        }

        public void IterationSetup()
        {
            _serializationOptions.ExternalObjectsByIds.Clear();
        }

        public void SerializeModel()
        {
            var bytes = MessagePackSerializer.Serialize(_person, _serializationOptions);
        }

        public void DeserializeModel()
        {
            var person = MessagePackSerializer.Deserialize<Person>(_serialized, _deserializationOptions);
        }

        public void Setup(int size)
        {
            _person = CreateModel(size);

            _serialized = MessagePackSerializer.Serialize(_person, _serializationOptions);
            _deserializationOptions.ExternalObjectsByIds = new Dictionary<int, object>(_serializationOptions.ExternalObjectsByIds);
        }

        private Person CreateModel(int petsCount)
        {
            var tima = new Pet { Name = "Tima", Power = 7.9f };

            var externalObject = new ExternalObject()
            {
                Quality = 7,
                Values = new Dictionary<string, object>
                {
                    {"fideliy", "important"}
                }
            };

            var person = new Person
            {
                Name = "Alex",
                Age = 28,
                Pets = CreatePetsWithDerived(petsCount, externalObject),
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
                },
                ExternalObject = externalObject
            };

            return person;
        }

        private List<Pet> CreatePetsWithDerived(int count, ExternalObject externalObject)
        {
            return Enumerable.Range(0, count).Select(index =>
            {
                Pet pet;
                if (index % 4 == 0)
                {
                    pet = new SuperPet
                    {
                        Name = ModelGeneration.GenerateName(10 + count % 10),
                        Power = index,
                        ExternalObject = index % 3 == 0 ? externalObject : null,
                        Kind = index + 10
                    };
                }
                else
                {
                    pet = new Pet
                    {
                        Name = ModelGeneration.GenerateName(10 + count % 10),
                        Power = index,
                        ExternalObject = index % 3 == 0 ? externalObject : null
                    };
                }

                return pet;
            }).ToList();
        }
    }
}
