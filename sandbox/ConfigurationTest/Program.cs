using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace ConfigurationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            RegisterResolvers();

            var tima = new Pet {Name = "Tima", Power = 7.9f};

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
                Numbers = new [] { 3, 9, 17, 32},
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
