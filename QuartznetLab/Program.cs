using System;
using System.Net.WebSockets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

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
              
             
               var job = JobBuilder.Create<HelloJob>().Build();
            
             
              var trigger = TriggerBuilder.Create().WithSimpleSchedule(
                  x => x.WithIntervalInSeconds(1).RepeatForever()).Build();
             
              await scheduler.ScheduleJob(job, trigger);
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