using Autofac;
using Autofac.Extras.Quartz;
using Autofac.Integration.Mvc;
using Quartz;
using Quartz.Impl;

namespace JuCheap.Web.Jobs
{
    public class EmailJobScheduler
    {
        public void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

            var scoppe = AutofacDependencyResolver.Current.RequestLifetimeScope.Resolve<AutofacJobFactory>();
            scheduler.JobFactory = scoppe;

            scheduler.Start();
            IJobDetail job = JobBuilder.Create<IJob>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("triggerName", "groupName")
                .WithSimpleSchedule(t =>
                    t.WithIntervalInSeconds(120)
                        .RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}