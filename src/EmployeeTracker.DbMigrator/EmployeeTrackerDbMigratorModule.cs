using EmployeeTracker.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace EmployeeTracker.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(EmployeeTrackerEntityFrameworkCoreModule),
    typeof(EmployeeTrackerApplicationContractsModule)
    )]
public class EmployeeTrackerDbMigratorModule : AbpModule
{
}
