using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeTracker.Projects;

public class CreateProjectDto :IValidatableObject
{
    public string Name { get; set; }

    [CanBeNull]
    public string? Description { get; set; }

    public string[]? LeaderNames { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
    {
        if (StartTime < new DateTime(DateTime.Now.Year - 50, DateTime.Now.Month, DateTime.Now.Day))
        {
            yield return new ValidationResult(
                "Start date must be higher than or equal to " +
                new DateTime(DateTime.Now.Year - 50, DateTime.Now.Month, DateTime.Now.Day).ToString(),
                new[] { "StartTime" }
                );
        }
        if (EndTime <= new DateTime(DateTime.Now.Year - 50, DateTime.Now.Month, DateTime.Now.Day)){


            yield return new ValidationResult(
                "End date must be higher than " +
                new DateTime(DateTime.Now.Year - 50, DateTime.Now.Month, DateTime.Now.Day).ToString(),
                new[] { "EndTime" }
                );
        }
        if (StartTime <= EndTime)
        {
            yield return new ValidationResult(
                "Start date must be lower than end date",
                new[] { "StartTime", "EndTime" }
                );
        }
    }
}
