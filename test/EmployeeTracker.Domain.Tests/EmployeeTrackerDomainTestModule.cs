using EmployeeTracker.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EmployeeTracker;

[DependsOn(
    typeof(EmployeeTrackerEntityFrameworkCoreTestModule)
    )]
public class EmployeeTrackerDomainTestModule : AbpModule
{

}
