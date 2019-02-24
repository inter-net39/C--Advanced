using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using LibToMeasures;
using System;

namespace BenchmarkingProject
{
    [MemoryDiagnoser]
    public class Program
    {
        [Benchmark]
        public void CalculateByteArray()
        {
            var result = Arrays.CalculateArrayBytes(10000, 100);
        }
        [Benchmark]
        public void CalculateListBytes()
        {
            var result = Arrays.CalculateListBytes(10000, 100);
        }
        [Benchmark]
        public void CalculateListBytesWithInitialCapacity()
        {
            var result = Arrays.CalculateListBytesWithInitialCapacity(10000, 100);
        }


        private static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Program>();
            // |                                Method |       Mean |     Error |    StdDev | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
            // |-------------------------------------- |-----------:|----------:|----------:|------------:|------------:|------------:|--------------------:|
            // |                    CalculateByteArray |   903.8 us |  21.57 us |  27.27 us |           - |           - |           - |               120 B |
            // |                    CalculateListBytes | 4,928.5 us |  98.26 us | 233.53 us |    492.1875 |    492.1875 |    492.1875 |           2101408 B |
            // | CalculateListBytesWithInitialCapacity | 5,222.2 us | 102.05 us | 109.19 us |    992.1875 |    992.1875 |    992.1875 |           3280536 B |

            Console.ReadKey();

        }
    }
}
