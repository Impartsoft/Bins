using Quartz.Logging;
using System;
using System.Threading.Tasks;
using static QuartzDemo.ConsoleLogProviders;

namespace QuartzDemo
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

            // 简单job启动
            //await QuartzTest1.QuartzTest();
            // 多job启动
            await QuartzTest2.QuartzTest();

            Console.ReadKey();
        }
    }
}
