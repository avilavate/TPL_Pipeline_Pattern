using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel_With_Immutable_Types
{
    class Program
    {
        static void Main(string[] args)
        {
            string immutableName = "0";
            var t0 = Task.Factory.StartNew(() =>
            {
                var rd = new Random(1).Next(1000, 5000);
                Thread.Sleep(rd);
                immutableName = "1";
              
            });

            var t1 = Task.Factory.StartNew(() =>
            {
                var rd = new Random(1).Next(1000, 5000);
                Thread.Sleep(rd);
                immutableName = "2";
                IEnumerable<int> nums = Enumerable.Concat(new List<int> { 1, 2, 3, 4, 5 }, Enumerable.Empty<int>());

                List<int> num1 = new List<int> { 1, 2, 3, 4, 5, 6 };

                Parallel.ForEach(Partitioner.Create(nums), (i) => { Console.WriteLine(i); });
            });

            Task.WaitAll(new Task[] { t0, t1 });
           // Console.WriteLine(immutableName);

            Console.Read();
        }
    }
}
