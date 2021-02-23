



## 0.介绍
> Open-source job scheduling system for .NET

Quartz.net 是调度任务框架，我们可以用来定时发送邮件、定时处理邮件、定时统计分析数据、定时监控...

本文介绍Quartz.net的简单使用

## 1. 参考资料
> 官方Doc https://www.quartz-scheduler.net/documentation/quartz-3.x/quick-start.html

> 博客 https://www.cnblogs.com/z-huan/p/7412181.html

> 远程调度GitHub https://github.com/guryanovev/CrystalQuartz



## 2.核心内容
##### 使用总结
Quartz.net 启用方式大致分为三种
- 程序配置

- XML方式配置


###### XML方式配置job可以参考
>  https://www.cnblogs.com/z-huan/p/7412181.html

- 远程调度

###### 远程调度job可以参考GitHub
>  https://github.com/guryanovev/CrystalQuartz


##### 程序配置简单使用

-  简单启用一个job
```
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
```

-  简单启用多个job
```
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
```

-  触发器使用Cron语法

```
ITrigger goodBayTrigger = TriggerBuilder.Create()
        .WithIdentity("GoodBayTrigger", "GoodBayJobGroup")
        .StartNow()
        .WithCronSchedule("0/10 * * * * ?")
        .Build();
```

更多Cron语法可参考
>  https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/crontrigger.html#introduction
        


-  IJob实现任务
```
    [DisallowConcurrentExecution]
    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Greetings from HelloJob!");
        }
    }
```
为job增加了特性[DisallowConcurrentExecution]，可以保证当前job还未执行结束前，就算触发时间已经到了，也不能再次开启job。保证job的工作是串行进行，而非并行。在实践过程中要根据具体情况考虑是否需要增加此特性（如果还无法确定是否需要添加，建议都添加上此特性~）
> [DisallowConcurrentExecution] is an attribute that can be added to the Job class that tells Quartz not to execute multiple instances of a given job definition (that refers to the given job class) concurrently.

## 3.样例源码地址

 > https://github.com/Impartsoft/Bins/tree/main/QuartzDemo

---


> 欢迎大家批评指正，共同学习，共同进步！
> 作者：Iannnnnnnnnnnnn
> 出处：https://www.cnblogs.com/Iannnnnnnnnnnnn
> 本文版权归作者和博客园共有，欢迎转载，但未经作者同意必须保留此段声明，且在文章页面明显位置给出原文连接，否则保留追究法律责任的权利。