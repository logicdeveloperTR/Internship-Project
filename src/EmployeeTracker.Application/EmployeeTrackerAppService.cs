using System;
using System.Collections.Generic;
using System.Text;
using EmployeeTracker.Localization;
using Volo.Abp.Application.Services;

namespace EmployeeTracker;

/* Inherit your application services from this class.
 */
public abstract class EmployeeTrackerAppService : ApplicationService
{
    protected EmployeeTrackerAppService()
    {
        LocalizationResource = typeof(EmployeeTrackerResource);
    }
}
