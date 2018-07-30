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

            var person = new Person
            {
                Name = "Alex",
                Age = 28,
                Pets = new List<Pet>
                {
                    new Pet { Name = "Jack", Power = 11f },
                    new Pet { Name = "Tima", Power = 7.9f },
                },
                Numbers = new [] { 3, 9, 17, 32}
            };

            var bytes = MessagePackSerializer.Serialize(person);

            Console.WriteLine(Convert.ToBase64String(bytes));
            Console.WriteLine(MessagePackSerializer.ToJson(bytes));

            var deserializedPerson = MessagePackSerializer.Deserialize<Person>(bytes);
            Console.WriteLine(deserializedPerson);
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
