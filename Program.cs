using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipelines
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> lines = new List<string>();
            var fileParsingTask = Task.Factory.StartNew(() =>
            {
                return File.ReadFromFile("fuel.csv");
            });
            try
            {
                lines = fileParsingTask.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot parse file...", ex.Message);
                Console.Read();
                return;
            }
            var buffer1 = Buffers.GetBuffer(lines.Count());
            var buffer2 = Buffers.GetBuffer(lines.Count());

            Program p = new Program();
            var t0 = p.ReadStrings(buffer1, lines);
            var t1 = Task.Factory.StartNew(() =>
            {
                List<string> buffer1Lines = new List<string>();
                try
                {
                    foreach (var item in buffer1.GetConsumingEnumerable())
                    {
                        buffer2.Add(item.ToUpper());
                    }
                }
                finally
                {
                    buffer2.CompleteAdding();
                }
            });
           
            var t2 = Task.Factory.StartNew(() =>
            {
                using (StreamWriter output = new StreamWriter("./uppercasedfuel.csv"))
                {
                    foreach (var text in buffer2.GetConsumingEnumerable())
                    {
                        output.WriteLine(text);
                    }
                }

            });

            Task.WaitAll(t0, t1, t2);
            //foreach (var item in buffer2)
            //{
            //    Console.WriteLine(item);
            //}
            Console.WriteLine("Done...");
            Console.Read();
        }



        public Task ReadStrings(BlockingCollection<string> buffer, List<string> lines)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    Parallel.ForEach(lines, (line) =>
                    {
                        buffer.Add(line);
                    });
                }
                finally
                {
                    buffer.CompleteAdding();
                }

            });
        }
    }
}
