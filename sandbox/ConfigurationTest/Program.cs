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
                        Kind = 1
                    },
                    tima
                },
                Numbers = new [] { 3, 9, 17, 32}
            };

            var bytes = MessagePackSerializer.Serialize(person);

            Console.WriteLine(Convert.ToBase64String(bytes));
            Console.WriteLine(MessagePackSerializer.ToJson(bytes));

            var deserializedPerson = MessagePackSerializer.Deserialize<Person>(bytes);
            Console.WriteLine(deserializedPerson);

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
