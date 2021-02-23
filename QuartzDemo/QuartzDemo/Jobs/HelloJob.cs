using Quartz;
using System;
using System.Threading.Tasks;

namespace QuartzDemo
{
    [DisallowConcurrentExecution]
    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Greetings from HelloJob!");
        }
    }
}
