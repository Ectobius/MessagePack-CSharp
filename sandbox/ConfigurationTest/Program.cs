using MessagePack;
using System;
using System.Collections.Generic;
using TestModels;

namespace ConfigurationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            RegisterResolvers();

            TestPopulating();
        }

        static void TestPopulating()
        {
            Console.WriteLine("Test Populating.........");

            var person = CreatePerson();
            var ultimateLucky = new UltimatePet { Name = "Ultimate Lucky", Power = 17.9f, Kind = 999, UberPowerName = "Mother Earth" };
            person.Pets[0] = ultimateLucky;

            var prevFavorite = person.FavoritePet;
            person.FavoritePet = ultimateLucky;

            var serializationOptions = new SerializationOptions
            {
                ExternalReferenceChecker = (obj) => obj.GetType() == typeof(ExternalObject)
            };
            var bytes = MessagePackSerializer.Serialize(person, serializationOptions);

            Console.WriteLine(MessagePackSerializer.ToJson(bytes));

            var emptyPerson = new Person();
            var deserializationOptions = new DeserializationOptions
            {
                ExternalObjectsByIds = serializationOptions.ExternalObjectsByIds
            };

            
            person.FavoritePet = prevFavorite;

            var firstPet = person.Pets[0];
            firstPet.Name = "Changed Name";

            person.Pets.RemoveAt(person.Pets.Count - 1);

            person.Dudes[0].Name = "Some another dude";

            person.LabeledPets["fieldy"].Name = "Some another pet";
            person.LabeledPets.Remove("green");
                
            var prevPerson = person;
            MessagePackSerializer.Populate(ref person, bytes, deserializationOptions);

            Console.WriteLine("Person is the same: {0}", ReferenceEquals(person, prevPerson));

            Console.WriteLine("First pet is the same: {0}", person.Pets[0] == ultimateLucky);
            Console.WriteLine("Favorite pet is reused: {0}", person.FavoritePet == ultimateLucky);

            Console.WriteLine("Pets count: {0}", person.Pets.Count);
        }

        static Person CreatePerson()
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
                Pets = new List<Pet>
                {
                    new Pet { Name = "Jack", Power = 11f },
                    tima,
                    new SuperPet
                    {
                        Name = "Miu",
                        Power = 99999f,
                        Kind = 1,
                        ExternalObject = externalObject
                    },
                    tima
                },
                FavoritePet = new SuperPet { Name = "Super Lucky", Power = 170.9f, Kind = 67 },
                Numbers = new[] { 3, 9, 17, 32 },
                ExternalObject = externalObject,
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

        static void AllStuff()
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
                Pets = new List<Pet>
                {
                    new Pet { Name = "Jack", Power = 11f },
                    tima,
                    new SuperPet
                    {
                        Name = "Miu",
                        Power = 99999f,
                        Kind = 1,
                        ExternalObject = externalObject
                    },
                    tima
                },
                FavoritePet = new Pet { Name = "Lucky", Power = 17.9f },
                Numbers = new[] { 3, 9, 17, 32 },
                ExternalObject = externalObject
            };

            var serializationOptions = new SerializationOptions
            {
                ExternalReferenceChecker = (obj) => obj.GetType() == typeof(ExternalObject)
            };
            var bytes = MessagePackSerializer.Serialize(person, serializationOptions);

            Console.WriteLine(Convert.ToBase64String(bytes));
            Console.WriteLine(MessagePackSerializer.ToJson(bytes));

            foreach (var key in serializationOptions.ExternalObjectsByIds.Keys)
            {
                Console.WriteLine("External object ID: {0}", key);
            }

            var deserializationOptions = new DeserializationOptions
            {
                ExternalObjectsByIds = serializationOptions.ExternalObjectsByIds
            };
            var deserializedPerson = MessagePackSerializer.Deserialize<Person>(bytes, deserializationOptions);
            Console.WriteLine(deserializedPerson);
            Console.WriteLine("External object present: {0}", deserializedPerson.ExternalObject != null);

            foreach (var pet in deserializedPerson.Pets)
            {
                Console.WriteLine(pet);
            }

            Console.WriteLine(deserializedPerson.Pets[1] == deserializedPerson.Pets[3]);

            Console.WriteLine("Let's corrupt some info...");

            person.Name = "Farman";
            person.FavoritePet.Power = 0f;

            Console.WriteLine(person);
            Console.WriteLine(person.FavoritePet);

            var originalPerson = person;
            var originalPet = person.FavoritePet;
            MessagePackSerializer.Populate(ref person, bytes, deserializationOptions);
            Console.WriteLine(person);
            Console.WriteLine(person.FavoritePet);
            Console.WriteLine("Person is original person: {0}", person == originalPerson);
            Console.WriteLine("Pet is original pet: {0}", person.FavoritePet == originalPet);
        }

        static void RegisterResolvers()
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
    }
}
