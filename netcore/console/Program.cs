using System;
using BenchmarkDotNet.Running;
using console.Enumerations.Example1;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("running...");

            var summary = BenchmarkRunner.Run<MyEnumerableBenchmark>();
        }
    }
}
