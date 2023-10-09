using EmployeeTracker.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EmployeeTracker.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class EmployeeTrackerPageModel : AbpPageModel
{
    protected EmployeeTrackerPageModel()
    {
        LocalizationResourceType = typeof(EmployeeTrackerResource);
    }
}
