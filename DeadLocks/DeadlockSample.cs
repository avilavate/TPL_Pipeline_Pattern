using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeadLocks
{
   public class DeadlockSample
    {
     public static void Deadlock()
        {
            object obj1 = new object();
            object obj2 = new object();
            Parallel.Invoke(
            () =>
            {
                for (int i = 0; ; i++)
                {
                    lock (obj1)
                    {
                        Console.WriteLine("T1: Got 1 at {0}", i);
                        lock (obj2)
                        Console.WriteLine("T1: Got 2 at {0}", i);
                    }
                }
            },
                () =>
                {
                    for (int i = 0; ; i++)
                    {
                        lock (obj2)
                        {
                            Console.WriteLine("T2: Got 2 at {0}", i);
                            lock (obj1)
                            Console.WriteLine("T2: Got 1 at {0}", i);
                        }
                    }
                });
        }
    }
}
