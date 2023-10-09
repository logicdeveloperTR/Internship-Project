using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EmployeeTracker.Projects;

public class ProjectDto : EntityDto<Guid>
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public string[]? LeaderNames { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
}
