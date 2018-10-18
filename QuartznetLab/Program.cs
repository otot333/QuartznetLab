using System;
using System.Net.WebSockets;
using System.Collections;
using System.Collections.Generic;
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
        static void Main(string[] args)
        {
            Console.WriteLine("Start QuartzNet Test...");

            Task.Run(async () => await InitializationScheduler());

            Console.WriteLine("Hello World!!!");
            Console.ReadLine();
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