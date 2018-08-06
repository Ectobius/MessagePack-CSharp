using System;
using System.Collections.Generic;
using System.Linq;
using MessagePack;
using TestModels;

namespace BenchmarkCustomized
{
    public class Benchmark
    {
        private Person _person;
        private SerializationOptions _serializationOptions;
        private DeserializationOptions _deserializationOptions;
        private byte[] _serialized;

        public static void RegisterResolvers()
        {
            MessagePack.Resolvers.CompositeResolver.RegisterAndSetAsDefault(
                // use generated resolver first, and combine many other generated/custom resolvers
                Resolvers.GeneratedResolver.Instance,

                // finally, use builtin/primitive resolver(don't use StandardResolver, it includes dynamic generation)
                MessagePack.Resolvers.BuiltinResolver.Instance,
                MessagePack.Resolvers.AttributeFormatterResolver.Instance,
                MessagePack.Resolvers.PrimitiveObjectResolver.Instance
            );
        }

        public Benchmark()
        {
            _serializationOptions = new SerializationOptions
            {
                ExternalReferenceChecker = (obj) => obj.GetType() == typeof(ExternalObject)
            };

            _deserializationOptions = new DeserializationOptions();
        }

        public void PrepareOptions()
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

        public void CreateModel(int petsCount)
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
                Pets = CreatePets(petsCount, externalObject),
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

            _person = person;

            _serialized = MessagePackSerializer.Serialize(_person, _serializationOptions);
            _deserializationOptions.ExternalObjectsByIds = _serializationOptions.ExternalObjectsByIds;
        }

        private List<Pet> CreatePets(int count, ExternalObject externalObject)
        {
            return Enumerable.Range(0, count).Select(index =>
            {
                Pet pet;
                if (index % 4 == 0)
                {
                    pet = new SuperPet
                    {
                        Name = GenerateName(10 + count % 10),
                        Power = index,
                        ExternalObject = index % 3 == 0 ? externalObject : null,
                        Kind = index + 10
                    };
                }
                else
                {
                    pet = new Pet
                    {
                        Name = GenerateName(10 + count % 10),
                        Power = index,
                        ExternalObject = index % 3 == 0 ? externalObject : null
                    };
                }
                
                return pet;
            }).ToList();
        }

        private string GenerateName(int length)
        {
            return string.Join("", Enumerable.Range(0, length).Select(i => 'a'));
        }
    }
}
