using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("开始");

            File[] cs = new File[2];
            cs[0] = new File { Path = "path1" };
            cs[1] = new File { Path = "path2" };
            var file = new File();
            file.Children = cs;

            var files = file.GetFiles();

            foreach (var tfile in files)
                Console.WriteLine(tfile.Path);

            Console.WriteLine("结束");

            File2[] cs2 = new File2[2];
            cs2[0] = new File2 { Path = "path21" };
            cs2[1] = new File2 { Path = "path22" };
            var file2 = new File2();
            file2.Children = cs2;

            var file2s = file2.GetFiles();
            foreach (var tfile in file2s)
                Console.WriteLine(tfile.Path);

            // dosomething
            Console.WriteLine("方法一、当前线程ID:" + Thread.CurrentThread.ManagedThreadId);
            await DoSomething();
            Console.WriteLine("方法二、当前线程ID:" + Thread.CurrentThread.ManagedThreadId);
            // dosomething
            Console.ReadLine();
        }

        private async static Task DoSomething()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("方法三、当前线程ID:" + Thread.CurrentThread.ManagedThreadId);
            });
        }
    }

    public static class Extentions
    {
        internal static TaskAwaiter GetAwaiter(this int milliseconds) => Task.Delay(milliseconds).GetAwaiter();

        public static TaskAwaiter GetAwaiter(this TimeSpan timeSpan)
        {
            return Task.Delay(timeSpan).GetAwaiter();
        }
    }
}
