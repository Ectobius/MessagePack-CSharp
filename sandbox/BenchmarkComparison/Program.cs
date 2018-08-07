using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace BenchmarkComparison
{
    [CoreJob]
    public class MessagePackCustomizedVsOriginal
    {
        private BenchmarkCustomized.BenchmarkSimpleModel _customizedSimpleModel = new BenchmarkCustomized.BenchmarkSimpleModel();
        private BenchmarkCustomized.BenchmarkComplexModel _customizedComplexModel = new BenchmarkCustomized.BenchmarkComplexModel();
        private BenchmarkOriginal.Benchmark _original = new BenchmarkOriginal.Benchmark();
        private BenchmarkJsonNet.Benchmark _jsonNet = new BenchmarkJsonNet.Benchmark();

        [Params(10, 100, 1000)]
        public int PetsCount;

        [GlobalSetup]
        public void Setup()
        {
            BenchmarkCustomized.Setup.RegisterResolvers();

            _customizedSimpleModel.Setup(PetsCount);
            _customizedComplexModel.Setup(PetsCount);
            _original.CreateModel(PetsCount);
            _jsonNet.CreateModel(PetsCount);
        }

        [IterationSetup]
        public void IterationSetup()
        {
            _customizedSimpleModel.IterationSetup();
            _customizedComplexModel.IterationSetup();
        }

        [Benchmark]
        public void SerializeCustomizedSimpleModel() => _customizedSimpleModel.SerializeModel();

        [Benchmark]
        public void SerializeCustomizedComplexModel() => _customizedComplexModel.SerializeModel();

        [Benchmark]
        public void SerializeOriginal() => _original.SerializeModel();

        [Benchmark]
        public void SerializeJsonNet() => _jsonNet.SerializeModel();

        [Benchmark]
        public void DeserializeCustomizedSimpleModel() => _customizedSimpleModel.DeserializeModel();

        [Benchmark]
        public void PopulateSimpleModel() => _customizedSimpleModel.PopulateModel();

        [Benchmark]
        public void DeserializeCustomizedComplexModel() => _customizedComplexModel.DeserializeModel();

        [Benchmark]
        public void DeserializeOriginal() => _original.DeserializeModel();

        [Benchmark]
        public void DeserializeJsonNet() => _jsonNet.DeserializeModel();
    }

    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<MessagePackCustomizedVsOriginal>();
        }
    }
}
