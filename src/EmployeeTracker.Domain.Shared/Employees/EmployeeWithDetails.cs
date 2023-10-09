using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;

namespace EmployeeTracker.Employees;

public class EmployeeWithDetails : IHasCreationTime
{
    public Guid Id{ get; set; }
    public string Name { get; set; }

    public string? Description { get; set; }

    [CanBeNull]
    public string []? HeadNames { get; set; }

    [CanBeNull]
    public string? HeadName { get; set; }   
    public EmployeeDepartment Department { get; set; }

    public double Salary { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime CreationTime{ get; set;}

}
