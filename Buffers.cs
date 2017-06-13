using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pipelines
{
    class Buffers
    {
        public static BlockingCollection<string> GetBuffer(int BufferSize = 0)
        {
            CancellationTokenSource ct = new CancellationTokenSource();
            return new BlockingCollection<string>(BufferSize);
        }
    }
}
