using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EmployeeTracker.Employees;

public class EmployeeDto : EntityDto<Guid>
{
    public string Name { get; set; }    
    public string? Description { get; set; }

    [CanBeNull]
    public string? HeadName { get; set; }

    [CanBeNull]
    public string[]? HeadNames { get; set; }
    public EmployeeDepartment Department { get; set; }
    public double Salary { get; set; }
    public DateTime StartTime { get; set; } 

}
