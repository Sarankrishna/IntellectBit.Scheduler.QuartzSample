using Common.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IntellectBit.Scheduler.QuartzSample
{
    public class Program
    {
        public static async Task Main()
        {
            ILog log = LogManager.GetLogger(typeof(Program));

            // First we must get a reference to a scheduler
            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler sched = await sf.GetScheduler();
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

               // await sched.ScheduleJob(job, trigger);

                await sched.Start();
                Thread.Sleep(TimeSpan.FromMinutes(2));
            }
            catch (Exception ex)
            {

                throw;
            }

            finally
            {
                await sched.Shutdown(true);
            }
        }
    }
}
