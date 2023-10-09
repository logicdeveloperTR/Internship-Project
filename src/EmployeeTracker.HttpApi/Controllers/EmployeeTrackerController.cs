using EmployeeTracker.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EmployeeTracker.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class EmployeeTrackerController : AbpControllerBase
{
    protected EmployeeTrackerController()
    {
        LocalizationResource = typeof(EmployeeTrackerResource);
    }
}
