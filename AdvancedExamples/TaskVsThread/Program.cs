using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskVsThread
{
    class Program
    {
        static void Main(string[] args)
        {

            // Use Thread for LOOONG processes that
            Thread thread = new Thread(() =>
            {
                Console.WriteLine("Thread - Started");

                // Dont use Thread.Sleep(), use Thread.Wait()
                // Thread.Sleep(100);
                
            });
            thread.Start();

            // You can run only ONE task per Core
            // Having more Tasks than cores
            // you will have context switching bottleneck
            Task task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Task - Started"); 
            });



            Console.ReadKey();
        }
    }
}
