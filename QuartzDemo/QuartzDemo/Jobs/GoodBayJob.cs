using Quartz;
using System;
using System.Threading.Tasks;

namespace QuartzDemo
{
    public class GoodBayJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Greetings from GoodBayJob!");
        }
    }
}
