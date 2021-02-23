
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using QuartzDemo;
using System;
using System.Threading.Tasks;
using static QuartzDemo.ConsoleLogProviders;

namespace QuartzDemo
{
    public class QuartzTest1
    {
        public static async Task QuartzTest()
        {
            // 实例化调度 Scheduler
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            await scheduler.Start();

            // 定义任务
            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity("job1", "group1")
                .Build();

            // 定义触发器
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            // 传入job和trigger开始调度任务
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
