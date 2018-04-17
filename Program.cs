using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace NewParallel
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //MyParallel.ParallelFor(0, 5, (i) => print(i));

            List<int> list = new List<int>{11, 2, -1, 4, 52, 6, 7, 0, 9};

            //MyParallel.ParallelForEach(list, (i) => { i++; Console.WriteLine(i);} );

            ParallelOptions p = new ParallelOptions();
            p.MaxDegreeOfParallelism = 4;

            MyParallel.ParallelForEachWithOptions(list, p, (i) => print(i));



            void print(int i)
            {
                Console.WriteLine("Task {0} has started.", i);
                Thread.Sleep(2000);
                Console.WriteLine("Task {0} has ended.", i);

            }


            Console.Read();
        }
    }
}
