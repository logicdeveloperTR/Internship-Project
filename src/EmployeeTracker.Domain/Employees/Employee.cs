using EmployeeTracker.HeadLists;
using EmployeeTracker.LeaderLists;
using EmployeeTracker.Projects;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Features;

namespace EmployeeTracker.Employees;

public class Employee: FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; }

    [CanBeNull] public string? Description { get; set; }

    public Collection<HeadList> HeadNames { get; set; }
    
    [CanBeNull] public string? HeadName { get; set; }

    public EmployeeDepartment Department { get; set; }

    public double Salary { get; set; }  

    public DateTime StartTime { get; set; }

    private Employee()
    {

    }

    public Employee(Guid id,
        string name, 
        EmployeeDepartment department, 
        double salary, 
        DateTime startTime,
        [CanBeNull] string? headName = null,
        [CanBeNull] string? description=null) :base(id)
    {
        Name = name;
        Description = description;
        Department = department;
        Salary = salary;
        StartTime = startTime;
        HeadName = headName;
        HeadNames = new Collection<HeadList>();    
    }
    public void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), ProjectConsts.MaxStr);
    }
    public void AddHead(Guid headId)
    {
        Check.NotNull(headId, nameof(headId));

        if (IsInHeads(headId))
        {
            return;
        }
        HeadNames.Add(new HeadList(headId: headId, employeeId: Id));
    }

    private bool IsInHeads(Guid headId)
    {
        return HeadNames.Any(x => x.HeadId == headId);
    }
    public void RemoveHead(Guid headId)
    {

        Check.NotNull(headId, nameof(headId));
        if (!IsInHeads(headId))
        {
            return;
        }
        HeadNames.RemoveAll(x => x.HeadId == headId);
    }
    public void RemoveAllHeadsExceptGivenIds(List<Guid> headIds)
    {
        Check.NotNullOrEmpty(headIds, nameof(headIds));
        HeadNames.RemoveAll(x => !headIds.Contains(x.HeadId));
    }
    public void RemoveAllHeads()
    {
        HeadNames.RemoveAll(x => x.EmployeeId == Id);
    }
}
