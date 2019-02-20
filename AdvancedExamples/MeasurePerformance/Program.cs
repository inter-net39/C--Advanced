using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MeasurePerformance
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int reps = 10;
            int times = 100;

            #region Arrays
            Console.WriteLine("<-- Arrays -->");
            Console.WriteLine("<-- byte -->");
            Measure("Measure Span From Array", reps, () =>
            {
                var arraySpan = new Span<byte>(new byte[10000]);

                for (int i = 0; i < times; i++)
                {
                    for (int j = 0; j < arraySpan.Length; j++)
                    {
                        arraySpan[j]++;
                    }
                }
            });
            Measure("Measure Span Stackalloc", reps, () =>
            {
                Span<byte> stackSpan = stackalloc byte[10000];

                for (int i = 0; i < times; i++)
                {
                    for (int j = 0; j < stackSpan.Length; j++)
                    {
                        stackSpan[j]++;
                    }
                }
            });
            MeasureWithMemoryUsage("Measure Array", reps, () =>
            {
                var array = new byte[10000];

                for (int i = 0; i < times; i++)
                {
                    for (int j = 0; j < array.Length; j++)
                    {
                        array[j]++;
                    }
                }

                return array;
            });
            MeasureWithMemoryUsage("Measure List", reps, () =>
            {
                var list = new List<byte>(new byte[10000]);

                for (int i = 0; i < times; i++)
                {
                    for (int j = 0; j < list.Count; j++)
                    {
                        list[j]++;
                    }
                }

                return list;
            });
            MeasureWithMemoryUsage("Measure List(default)", reps, () =>
            {
                var list = new List<byte>();

                for (int i = 0; i < times; i++)
                {
                    for (int j = 0; j < times; j++)
                    {
                        list.Add(new byte());
                        list[j]++;
                    }
                }

                return list;
            });
            Console.WriteLine("<-- Long -->");
            Measure("MeasureSpanFromArray", reps, () =>
            {
                var arraySpan = new Span<long>(new long[10000]);

                for (int i = 0; i < times; i++)
                {
                    for (int j = 0; j < arraySpan.Length; j++)
                    {
                        arraySpan[j]++;
                    }
                }
            });
            Measure("MeasureSpanStackalloc", reps, () =>
            {
                Span<long> stackSpan = stackalloc long[10000];

                for (int i = 0; i < times; i++)
                {
                    for (int j = 0; j < stackSpan.Length; j++)
                    {
                        stackSpan[j]++;
                    }
                }
            });
            MeasureWithMemoryUsage("MeasureArray", reps, () =>
            {
                var array = new long[10000];

                for (int i = 0; i < times; i++)
                {
                    for (int j = 0; j < array.Length; j++)
                    {
                        array[j]++;
                    }
                }
                return array;
            });
            MeasureWithMemoryUsage("Measure List", reps, () =>
            {
                var list = new List<long>(new long[10000]);

                for (int i = 0; i < times; i++)
                {
                    for (int j = 0; j < list.Count; j++)
                    {
                        list[j]++;
                    }
                }

                return list;
            });
            MeasureWithMemoryUsage("Measure List(default)", reps, () =>
            {
                var list = new List<long>();

                for (int i = 0; i < times; i++)
                {
                    for (int j = 0; j < times; j++)
                    {
                        list.Add(new long());
                        list[j]++;
                    }
                }

                return list;
            });

            #endregion

            #region Strings

            Console.WriteLine("<--Strings-->");
            Measure("Measure string", reps, () =>
            {
                var text = string.Empty;

                for (int i = 0; i < times; i++)
                {
                    for (int j = 0; j < times; j++)
                    {
                        text += " a@\r" + " a@\r";
                    }
                }
            });
            Measure("Measure string$", reps, () =>
            {
                var text = string.Empty;
                var append = " a@\r";
                for (int i = 0; i < times; i++)
                {
                    for (int j = 0; j < times; j++)
                    {
                        text += $" a@\r{append}";
                    }
                }
            });
            Measure("Measure StringBuilder", reps, () =>
            {
                StringBuilder stringBuilder = new StringBuilder(string.Empty);

                for (int i = 0; i < times; i++)
                {
                    for (int j = 0; j < times; j++)
                    {
                        stringBuilder.Append(" a@\r");
                        stringBuilder.Append(" a@\r");
                    }
                }
            });

            #endregion

            #region References

            

            #endregion

            Console.ReadKey();
        }

        public static void Measure(string what, int reps, Action action)
        {

            GC.Collect(); // force to clean the memory
            GC.WaitForPendingFinalizers();
            GC.Collect();
            action(); // warm up

            double[] performance = new double[reps];

            for (int i = 0; i < reps; i++)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                action();
                performance[i] = stopwatch.Elapsed.TotalMilliseconds;
            }

            Console.WriteLine($"{what.PadLeft(30)} - AVG: {performance.Average()}, MIN: {performance.Min()}, MAX: {performance.Max()}");
        }
        public static void MeasureWithMemoryUsage<T>(string what, int reps, Func<T> action)
        {

            GC.Collect(); // force to clean the memory
            action(); // warm up

            double[] performance = new double[reps];
            long[,] memory = new long[reps, 2];

            for (int i = 0; i < reps; i++)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                memory[i, 0] = GC.GetTotalMemory(true);
                T result = action();
                memory[i, 1] = GC.GetTotalMemory(true);
                performance[i] = stopwatch.Elapsed.TotalMilliseconds;
            }
            long[] usedMemory = new long[reps];
            for (int i = 0; i < reps; i++)
            {
                usedMemory[i] = (memory[i, 1] - memory[i, 0]);
            }
            Console.WriteLine($"{what.PadLeft(30)} - AVG: {performance.Average()}, MIN: {performance.Min()}, MAX: {performance.Max()}\r\n{string.Empty.PadLeft(32)} AVG-MEMORY: {usedMemory.Average()}, MIN-MEMORY: {usedMemory.Min()}, MAX-MEMORY: {usedMemory.Max()}");
        }
    }
}
