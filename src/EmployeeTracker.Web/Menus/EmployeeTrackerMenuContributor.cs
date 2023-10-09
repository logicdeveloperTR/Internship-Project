using System.Threading.Tasks;
using EmployeeTracker.Localization;
using EmployeeTracker.MultiTenancy;
using EmployeeTracker.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace EmployeeTracker.Web.Menus;

public class EmployeeTrackerMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<EmployeeTrackerResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                EmployeeTrackerMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "EmployeeTracker",
                l["Menu:EmployeeTracker"],
                icon: "fas fa-eye"
            ).AddItem(
                new ApplicationMenuItem(
                    "EmployeeTracker.Employees",
                    l["Menu:Employees"],
                    url: "/Employees",
                    icon:"fas fa-users"
                ).RequirePermissions(EmployeeTrackerPermissions.Employees.Default)
            ).AddItem(
                new ApplicationMenuItem(
                    "EmployeeTracker.Projects",
                    l["Menu:Projects"],
                    icon:"fas fa-business-time",
                    url: "/Projects"
                ).RequirePermissions(EmployeeTrackerPermissions.Projects.Default)
            )
        );
        return Task.CompletedTask;
    }
}
