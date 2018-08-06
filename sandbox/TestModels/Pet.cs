namespace TestModels
{
    public class Pet
    {
        public string Name { get; set; }
        public float Power { get; set; }

        public ExternalObject ExternalObject { get; set; }

        public override string ToString()
        {
            return $"{nameof(Pet)}: {Name}, {Power}";
        }
    }
}
