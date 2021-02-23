
using Quartz;
using Quartz.Impl;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuartzDemo
{
    public class QuartzTest2
    {
        public static async Task QuartzTest()
        {
            // Grab the Scheduler instance from the Factory
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            // and start it off
            await scheduler.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail helloJob = JobBuilder.Create<HelloJob>()
                .WithIdentity("HelloJob", "HelloJobGroup")
                .Build();

            // define the job and tie it to our HelloJob class
            IJobDetail goodBayJob = JobBuilder.Create<GoodBayJob>()
                .WithIdentity("GoodBayJob", "GoodBayJobGroup")
                .Build();

            // Trigger the job to run now, and then repeat every 10 seconds
            ITrigger helloJobTrigger = TriggerBuilder.Create()
                .WithIdentity("HelloJobTrigger", "HelloJobGroup")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

              ITrigger goodBayTrigger = TriggerBuilder.Create()
                .WithIdentity("GoodBayTrigger", "GoodBayJobGroup")
                .StartNow()
                .WithCronSchedule("0/10 * * * * ?")
                .Build();

            var jobs = new Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>> {
                    { helloJob,new HashSet<ITrigger>() { helloJobTrigger }},
                    { goodBayJob,new HashSet<ITrigger>() { goodBayTrigger }},
                };

            await scheduler.ScheduleJobs(jobs, false);
        }
    }
}
