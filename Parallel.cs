using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewParallel
{
    public class MyParallel
    {
        public static void ParallelFor(int fromInclusive, int toExclusive, Action<int> body)
        {
            for (int i = fromInclusive; i < toExclusive; i++)
            {
                int container = i;
                Task.Run(() => { body(container); });
            }
        }

        public static void ParallelForEach<TSource>(IEnumerable<TSource> source, Action<TSource> body)
        {
            foreach (TSource item in source)
            {
                Task.Run(() => { body(item); });
            }
        }

        public static void ParallelForEachWithOptions<TSource>
        (IEnumerable<TSource> source, ParallelOptions options, Action<TSource> body)
        {
            using (SemaphoreSlim concurrencySemaphore = new SemaphoreSlim(options.MaxDegreeOfParallelism))
            {
                List<Task> tasks = new List<Task>();

                foreach (TSource item in source)
                {
                    concurrencySemaphore.Wait();

                    var t = Task.Factory.StartNew(() =>
                    {

                        try
                        {
                            body(item);
                        }
                        finally
                        {
                            concurrencySemaphore.Release();
                        }
                    });

                    tasks.Add(t);
                }

                Task.WaitAll(tasks.ToArray());
            }
        }
    }
}
