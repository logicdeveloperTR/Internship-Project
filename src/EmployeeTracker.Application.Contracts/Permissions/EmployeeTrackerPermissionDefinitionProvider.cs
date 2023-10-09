using EmployeeTracker.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EmployeeTracker.Permissions;

public class EmployeeTrackerPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(EmployeeTrackerPermissions.GroupName);

        var employeesPermission = myGroup.AddPermission(EmployeeTrackerPermissions.Employees.Default, L("Permission:Employees"));
        employeesPermission.AddChild(EmployeeTrackerPermissions.Employees.Create, L("Permission:Employees.Create"));
        employeesPermission.AddChild(EmployeeTrackerPermissions.Employees.Edit, L("Permission:Employees.Edit"));
        employeesPermission.AddChild(EmployeeTrackerPermissions.Employees.Delete, L("Permission:Employees.Delete"));

        var projectsPermission = myGroup.AddPermission(EmployeeTrackerPermissions.Projects.Default, L("Permission:Projects"));
        projectsPermission.AddChild(EmployeeTrackerPermissions.Projects.Create, L("Permission:Projects.Create"));
        projectsPermission.AddChild(EmployeeTrackerPermissions.Projects.Edit, L("Permission:Projects.Edit"));
        projectsPermission.AddChild(EmployeeTrackerPermissions.Projects.Delete, L("Permission:Projects.Delete"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(EmployeeTrackerPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EmployeeTrackerResource>(name);
    }
}
