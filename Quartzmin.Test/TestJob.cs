using System;
using System.Threading.Tasks;
using Quartz;

namespace Quartzmin.Test
{
    public class TestJob:IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("do nothing");
        }
    }
}