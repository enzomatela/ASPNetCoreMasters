using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Masters.Api.Filters
{
    public class ResourcePerformanceFilter : IResourceFilter
    {
        private readonly Stopwatch stopwatch = new Stopwatch();

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            stopwatch.Start();
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            stopwatch.Stop();
        }
    }
}
