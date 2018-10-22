using System;
using System.Net.WebSockets;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;

namespace QuartznetLab
{
    class Program
    {
        private static IScheduler sch;
        static async Task Main(string[] args)
        {
            Console.WriteLine("Start QuartzNet Test...");
            //Task.Run(async () => await InitializationScheduler());
            await InitializationSchedulerFromDataBase();
            
            Console.WriteLine("Hello World!!!");
            var key = Console.ReadLine();

            if (key == "stop")
            {
                Console.WriteLine("Key is Stop");
                sch.Shutdown();
            }
        }
        
        private static async Task InitializationSchedulerFromDataBase()
        {
              
            //1.首先创建一个作业调度池
            var properties = new NameValueCollection();
            //存储类型
            properties["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";
            //表明前缀
            properties["quartz.jobStore.tablePrefix"] = "QRTZ_";
            //驱动类型
            properties["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.MySQLDelegate, Quartz";
            //数据源名称
            properties["quartz.jobStore.dataSource"] = "myDS";
            //连接字符串
            properties["quartz.dataSource.myDS.connectionString"] = "Server=localhost; Port=3306; Database=QuartzNetMYSQL;Uid=root;Pwd=pass.123";            
            //版本
            properties["quartz.dataSource.myDS.provider"] = "MySql";
            properties["quartz.serializer.type"] = "json";
            properties["quartz.jobStore.clustered"] = "true";
            properties["quartz.scheduler.instanceId"] = "1";
            properties["quartz.scheduler.instanceName"] = "IAMQ";
            
            var schedulerFactory = new StdSchedulerFactory(properties);
            sch = await schedulerFactory.GetScheduler();

            
            //最大链接数
            //properties["quartz.dataSource.myDS.maxConnections"] = "5";
            // First we must get a reference to a scheduler
            
            var job2 = JobBuilder.Create<HelloJob>()
                .WithIdentity("Job2").Build();
            
            var trigger = TriggerBuilder.Create()
                .StartNow()
                .WithIdentity("Trigger1")
                .WithSimpleSchedule(x=>x.WithIntervalInSeconds(3).RepeatForever())
                .ForJob("Job2").Build();
            
            var jobExist = await sch.CheckExists(job2.Key);
            if (!jobExist)
            {
                await sch.ScheduleJob(job2, trigger);
            }
            //await scheduler.ScheduleJob(job2, trigger);
//            await scheduler.AddJob(job1, true);
//            await scheduler.ScheduleJob(trigger);
//            scheduler.ListenerManager.AddJobListener(
//                new MyJobListener(),
//                KeyMatcher<JobKey>.KeyEquals(new JobKey("Job1")));
            sch.Start();
              
        }

        private static async Task InitializationScheduler()
        {
              
              IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
             
              var job1 = JobBuilder.Create<HelloJob>()
                  .WithIdentity("Job1")
                  .StoreDurably(true).Build();
            
              var dinnerTime = DateBuilder.TodayAt(14, 33, 50);
              var trigger = TriggerBuilder.Create()
                .StartNow()
                  .WithSimpleSchedule(x=>x.WithIntervalInSeconds(3).RepeatForever())
                .ForJob("Job1").Build();
              await scheduler.AddJob(job1, true);
              await scheduler.ScheduleJob(trigger);
              scheduler.ListenerManager.AddJobListener(
                  new MyJobListener(),
                  KeyMatcher<JobKey>.KeyEquals(new JobKey("Job1")));
              await scheduler.Start();
        }
        
        
    }
    
    public class HelloJob : IJob
    {
        
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine(DateTime.Now.ToString("r"));
            return Task.Delay(0);

        }
    }


}