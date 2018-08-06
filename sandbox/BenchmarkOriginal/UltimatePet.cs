namespace BenchmarkOriginal
{
    public class UltimatePet : SuperPet
    {
        public string UberPowerName { get; set; }

        public override string ToString()
        {
            return $"{nameof(UltimatePet)}: {Name}, {Power}, {Kind}, {UberPowerName}";
        }
    }
}
