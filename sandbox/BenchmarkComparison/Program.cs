using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace BenchmarkComparison
{
    [CoreJob]
    public class MessagePackCustomizedVsOriginal
    {
        private BenchmarkCustomized.Benchmark _customized = new BenchmarkCustomized.Benchmark();
        private BenchmarkOriginal.Benchmark _original = new BenchmarkOriginal.Benchmark();
        private BenchmarkJsonNet.Benchmark _jsonNet = new BenchmarkJsonNet.Benchmark();

        [Params(10, 100, 1000)]
        public int PetsCount;

        [GlobalSetup]
        public void Setup()
        {
            BenchmarkCustomized.Benchmark.RegisterResolvers();

            _customized.CreateModel(PetsCount);
            _original.CreateModel(PetsCount);
            _jsonNet.CreateModel(PetsCount);
        }

        [IterationSetup]
        public void IterationSetup()
        {
            _customized.PrepareOptions();
        }

        [Benchmark]
        public void SerializeCustomized() => _customized.SerializeModel();

        [Benchmark]
        public void SerializeOriginal() => _original.SerializeModel();

        [Benchmark]
        public void SerializeJsonNet() => _jsonNet.SerializeModel();

        [Benchmark]
        public void DeserializeCustomized() => _customized.DeserializeModel();

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
