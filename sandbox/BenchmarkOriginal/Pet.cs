using MessagePack;

namespace BenchmarkOriginal
{
    [MessagePackObject]
    public class Pet
    {
        [Key(0)]
        public string Name { get; set; }

        [Key(1)]
        public float Power { get; set; }

        [Key(2)]
        public ExternalObject ExternalObject { get; set; }

        public override string ToString()
        {
            return $"{nameof(Pet)}: {Name}, {Power}";
        }
    }
}
