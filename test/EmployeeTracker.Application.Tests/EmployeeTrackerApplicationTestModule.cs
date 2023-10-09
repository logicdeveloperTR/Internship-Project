using Volo.Abp.Modularity;

namespace EmployeeTracker;

[DependsOn(
    typeof(EmployeeTrackerApplicationModule),
    typeof(EmployeeTrackerDomainTestModule)
    )]
public class EmployeeTrackerApplicationTestModule : AbpModule
{

}
