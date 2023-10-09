using System.Threading.Tasks;

namespace EmployeeTracker.Data;

public interface IEmployeeTrackerDbSchemaMigrator
{
    Task MigrateAsync();
}
