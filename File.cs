using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipelines
{
    class File
    {
        public List<string> Lines { get; set; }

        public static List<string> ReadFromFile(string path = @"./fuel.csv")
        {
            var lines = System.IO.File.ReadAllLines(path);
            return lines.ToList();
        }
    }
}
