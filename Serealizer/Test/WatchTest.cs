using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serealizer.Test
{
    public class WatchTest
    {
        int _loopLen;
        public WatchTest(int loopLen = 0)
        {
            _loopLen = loopLen > 0 ? loopLen : 100;
        }
        public delegate object WatchAction();
        public long Watch(WatchAction action)
        {
            var stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < _loopLen; i++)
            {
                action();
            }
            
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }


    }
}
