using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EmployeeTracker.Data;
using Volo.Abp.DependencyInjection;

namespace EmployeeTracker.EntityFrameworkCore;

public class EntityFrameworkCoreEmployeeTrackerDbSchemaMigrator
    : IEmployeeTrackerDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreEmployeeTrackerDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the EmployeeTrackerDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<EmployeeTrackerDbContext>()
            .Database
            .MigrateAsync();
    }
}
