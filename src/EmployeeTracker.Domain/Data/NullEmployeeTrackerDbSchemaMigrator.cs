using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EmployeeTracker.Data;

/* This is used if database provider does't define
 * IEmployeeTrackerDbSchemaMigrator implementation.
 */
public class NullEmployeeTrackerDbSchemaMigrator : IEmployeeTrackerDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
