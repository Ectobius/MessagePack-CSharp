namespace TestModels
{
    public class SuperPet : Pet
    {
        public int Kind { get; set; }

        public override string ToString()
        {
            return $"{nameof(SuperPet)}: {Name}, {Power}, {Kind}";
        }
    }
}
