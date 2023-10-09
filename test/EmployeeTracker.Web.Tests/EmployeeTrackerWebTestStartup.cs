using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace EmployeeTracker;

public class EmployeeTrackerWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<EmployeeTrackerWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
