using Common.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntellectBit.Scheduler.QuartzSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ILog log = LogManager.GetLogger(typeof(Program));

            // First we must get a reference to a scheduler
            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler sched = sf.GetScheduler().Result;

            try
            {
                var startTime = DateTimeOffset.Now.AddSeconds(5);

                var job = JobBuilder.Create<SimpleJob>()
                                    .WithIdentity("job1", "group1")
                                    .Build();

                var trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartAt(startTime)
                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever())
                    .Build();

                sched.ScheduleJob(job, trigger);

                sched.Start();
                Thread.Sleep(TimeSpan.FromMinutes(2));
            }
            finally
            {
                sched.Shutdown(true);
            }
        }
    }
}
