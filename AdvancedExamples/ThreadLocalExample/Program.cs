using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadLocalExample
{
    internal class Program
    {

        private static ThreadLocal<int> threadLocalData = new ThreadLocal<int>();
        private static int threadSharedData = new int();

        private static void Main(string[] args)
        {
            Task task1 = Task.Factory.StartNew(() =>
            {
                threadLocalData.Value = 1;
                threadSharedData = 1;

            });
            Task task2 = Task.Factory.StartNew(() =>
            {
                threadLocalData.Value = 2;
                threadSharedData = 2;

            });
            task1.Wait();
            task2.Wait();

            Console.WriteLine(threadLocalData);
            Console.WriteLine(threadSharedData);


            Console.ReadKey();
        }
    }
}
