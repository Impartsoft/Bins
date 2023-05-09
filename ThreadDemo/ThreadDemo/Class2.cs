using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadDemo
{
    internal class ValueTaskTest
    {
        public async ValueTask GetValue()
        {
            await Task.Delay(100);
        }

        public async Task GetValue2()
        {
            await Task.Delay(100);
        }
    }
}
