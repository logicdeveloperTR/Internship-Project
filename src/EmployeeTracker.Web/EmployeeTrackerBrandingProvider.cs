using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace EmployeeTracker.Web;

[Dependency(ReplaceServices = true)]
public class EmployeeTrackerBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "EmployeeTracker";
}
