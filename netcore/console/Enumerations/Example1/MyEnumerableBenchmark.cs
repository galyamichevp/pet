using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;

namespace console.Enumerations.Example1
{
    [CoreJob]
    [RPlotExporter, RankColumn]
    [MemoryDiagnoser]
    [Config(typeof(Config))]
    public class MyEnumerableBenchmark
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                Add(StatisticColumn.P0,
                    StatisticColumn.P25,
                    StatisticColumn.P50,
                    StatisticColumn.P67,
                    StatisticColumn.P80,
                    StatisticColumn.P85,
                    StatisticColumn.P90,
                    StatisticColumn.P95,
                    StatisticColumn.P100);
            }
        }
        
        [Params(1000, 10000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
        }

        [Benchmark]
        public void EmptyClass()
        {
            foreach (var x in MyEnumerable.EmptyClass<string>())
            {

            }
        }

        [Benchmark(Baseline = true)]
        public void EmptyDisposableStruct()
        {
            foreach (var x in MyEnumerable.EmptyDisposableStruct<string>())
            {

            }
        }

        [Benchmark]
        public void EmptyStruct()
        {
            foreach (var x in MyEnumerable.EmptyStruct<string>())
            {

            }
        }
    }
}