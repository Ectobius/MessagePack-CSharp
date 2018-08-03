using MessagePack.Configuration;

namespace ConfigurationTest
{
    public class TestTypeRegistration : TypeRegistration
    {
        public TestTypeRegistration()
        {
            Register(
                typeof(Person),
                typeof(Pet),
                typeof(SuperPet),
                typeof(UltimatePet)
            );
        }
    }
}
