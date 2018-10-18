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

            // DateTime类型
            DateTime date1 = DateTime.Parse("2018-01-01 11:45:30");
//DateTimeOffset类型
            DateTimeOffset date3 = DateBuilder.DateOf(11, 45, 30, 1, 1, 2018);
//1. DateTime 转换成 DateTimeOffset
            DateTimeOffset date16 = new DateTimeOffset(date1, TimeSpan.Zero);
//2. DateTimeOffset 转换成 DateTime
            //DateTime date17 = Convert.ToDateTime(date3);
            Console.WriteLine(date1);
            Console.WriteLine(date3);
            Console.WriteLine(date16);
            //Console.WriteLine(date17);
            
//            Task.Run(async () => await InitializationScheduler());
              

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