using Volo.Abp.Settings;

namespace EmployeeTracker.Settings;

public class EmployeeTrackerSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(EmployeeTrackerSettings.MySetting1));
    }
}
