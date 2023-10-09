using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeTracker.Employees;

public class CreateEmployeeDto : IValidatableObject
{
    public string Name { get; set; }
    public string? Description { get; set; }

    public string? HeadName { get; set; }
    public string[]? HeadNames { get; set; }
    public EmployeeDepartment Department { get; set; }

    [Range(1000, 100000)]
    public double Salary { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
    {
        if(StartTime<new DateTime(DateTime.Now.Year-50, DateTime.Now.Month, DateTime.Now.Day))
        {
            yield return new ValidationResult(
                "Start date must be higher or equal to "+
                new DateTime(DateTime.Now.Year-50, DateTime.Now.Month, DateTime.Now.Day).ToString(),
                new[] { "StartTime" }
                );
        }
        if (Name.Split("").Length == 0)
        {
            yield return new ValidationResult(
                "Name must not empty",
                new[] { "Name" }
                );
        }
    }
}
