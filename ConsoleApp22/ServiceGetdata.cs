using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApp22
{

    public class ServiceGetdata
    {
        public async void Start()
        {
            //var data = new Getdata();
            //data.CreateDatabase();
            //data.saveDatabase();
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity("job1", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();
        }
        [DisallowConcurrentExecution]
        public class HelloJob : IJob
        {
            public Task Execute(IJobExecutionContext context)
            {
                Console.WriteLine($"{DateTime.Now}fghjtyutyuimrtyujntu4ren");
                var data = new Getdata();
                data.CreateDatabase();
                data.saveDatabase();
                return Task.CompletedTask;
            }
        }
        public void Stop() {
           
        }
        
    }
}
