using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace Quartzmin.Test
{
    public class DependencyJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly ConcurrentDictionary<IJob, IServiceScope> _scopes =
            new ConcurrentDictionary<IJob, IServiceScope>();

        public DependencyJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobScope = _serviceProvider.CreateScope();
            try
            {
                var job = jobScope.ServiceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
                _scopes.TryAdd(job, jobScope);
                return job;
            }
            catch (InvalidOperationException)
            {
                jobScope.Dispose();
                throw;
            }
        }

        public void ReturnJob(IJob job)
        {
            if (_scopes.TryRemove(job, out var jobScope)) jobScope.Dispose();
        }
    }
}