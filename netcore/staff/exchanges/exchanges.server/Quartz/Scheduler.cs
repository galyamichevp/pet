using exchanges.server.Configurations.Entities;
using exchanges.server.Quartz.Jobs;
using exchanges.server.Resources;
using Microsoft.Extensions.Options;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exchanges.server.Quartz
{
    public class QuartzStartup
    {
        private const int LagInMsec = 1000;
        private IScheduler _scheduler;

        private readonly IServiceProvider _container;
        private readonly List<ProviderConfig> _providerConfigs;
        public QuartzStartup(IServiceProvider container, IOptions<List<ProviderConfig>> providerConfigs)
        {
            _container = container;
            _providerConfigs = providerConfigs.Value;
        }

        public void Start()
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();

            _scheduler = factory.GetScheduler().Result;
            _scheduler.JobFactory = new JobFactory(_container);

            _scheduler.Start().Wait();

            foreach (var provider in _providerConfigs.Where(pc => pc.IsEnabled))
            {
                if (provider.Name == Constants.Providers.LocalProvider)
                    RunLocal(provider.ExpiredInMsec);
                else if (provider.Name == Constants.Providers.OERProvider)
                    RunOer(provider.ExpiredInMsec);
            }
        }

        public void Stop()
        {
            if (_scheduler == null)
            {
                return;
            }

            if (_scheduler.Shutdown(waitForJobsToComplete: true).Wait(30000))
            {
                _scheduler = null;
            }
            else
            {
            }
        }

        private void RunLocal(int expiredInMsec)
        {
            IJobDetail job = JobBuilder.Create<LocalJob>()
                        .WithIdentity("localJob", "localGroup")
                        .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("localTrigger", "localGroup")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithInterval(TimeSpan.FromMilliseconds(expiredInMsec - LagInMsec))
                    .RepeatForever())
                .Build();

            _scheduler.ScheduleJob(job, trigger);
        }

        private void RunOer(int expiredInMsec)
        {
            IJobDetail job = JobBuilder.Create<OERJob>()
                        .WithIdentity("oerJob", "oerGroup")
                        .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("oerTrigger", "oerGroup")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithInterval(TimeSpan.FromMilliseconds(expiredInMsec - LagInMsec))
                    .RepeatForever())
                .Build();

            _scheduler.ScheduleJob(job, trigger);
        }
    }
}