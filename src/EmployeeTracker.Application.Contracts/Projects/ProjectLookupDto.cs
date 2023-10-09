using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectExtending.Modularity;

namespace EmployeeTracker.Projects;

public class ProjectLookupDto:EntityDto<Guid>
{
    public string Name { get; set; }
}
