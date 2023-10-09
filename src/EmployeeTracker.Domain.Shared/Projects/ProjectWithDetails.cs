using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;

namespace EmployeeTracker.Projects;

public class ProjectWithDetails:IHasCreationTime
{

    public Guid Id { get; set; }

    [NotNull]
    public string Name { get; set; }

    [CanBeNull]
    public string? Description { get; set; }

    [CanBeNull]
    public string[]? LeaderNames { get; set; }

    [NotNull]
    public DateTime StartTime { get; set; }
    [NotNull]
    public DateTime EndTime { get; set; }

    public DateTime CreationTime { get; set; }

}
