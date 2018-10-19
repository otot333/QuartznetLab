using System;
using System.Threading;
using System.Threading.Tasks;
using Quartz;

namespace QuartznetLab
{
    public class MyJobListener :IJobListener
    {
        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            //執行前
            Console.WriteLine("before");
            
        }

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            
            return Task.Delay(0);
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException,
            CancellationToken cancellationToken = new CancellationToken())
        {
            //執行前
            Console.WriteLine("after");
            return Task.Delay(0);
        }

        public string Name => "i am Name";
    }
}