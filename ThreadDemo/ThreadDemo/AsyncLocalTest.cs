using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDemo
{
    public static class AsyncLocalTest
    {
        public static async Task AsyncLocalTest1()
        {
            AsyncLocal<int> asyncLocal = new();
            asyncLocal.Value = 1;

            await WorkAsync();
            Console.WriteLine(nameof(AsyncLocalTest1) + "：" + asyncLocal.Value);

            async Task WorkAsync()
            {
                Console.WriteLine(nameof(WorkAsync) + "Start：" + asyncLocal.Value);

                asyncLocal.Value = 2;

                Console.WriteLine(nameof(WorkAsync) + "End：" + asyncLocal.Value);
            }
        }

        static int The_int = 0;

        public static async Task AsyncLocalTest3()
        {
            The_int = 2;

            await WorkAsync3();
        }

        private static async Task WorkAsync3()
        {
            Console.WriteLine(nameof(WorkAsync3) + "：" + The_int);
        }


        static AsyncLocal<int>  The_AsyncLocal = new();
     
        public static async Task AsyncLocalTest2()
        {
            The_AsyncLocal.Value = 2;

            await WorkAsync2();
        }

        private static async Task WorkAsync2()
        {
            Console.WriteLine(nameof(WorkAsync2) + "：" + The_AsyncLocal.Value);
        }
    }
}
