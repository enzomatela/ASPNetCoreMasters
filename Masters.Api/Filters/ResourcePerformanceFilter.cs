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
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.HttpContext.Items["Stopwatch"] = new Stopwatch(); ; // store​
            Stopwatch stopwatch = (Stopwatch)context.HttpContext.Items["Stopwatch"]; // retrieve
            stopwatch.Start();
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Stopwatch stopwatch = (Stopwatch)context.HttpContext.Items["Stopwatch"]; // retrieve
            stopwatch.Stop();
        }
    }
}
