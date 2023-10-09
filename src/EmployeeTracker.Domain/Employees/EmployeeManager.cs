using EmployeeTracker.Employees;
using EmployeeTracker.Heads;
using EmployeeTracker.Leaders;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace EmployeeTracker.Employees;

public class EmployeeManager : DomainService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IRepository<Head, Guid> _headRepository;
    private readonly IRepository<Leader, Guid> _leaderRepository;

    public EmployeeManager(IEmployeeRepository employeeRepository,
        IRepository<Head, Guid> headRepository,
        IRepository<Leader, Guid> leaderRepository
        )
    {
        _employeeRepository = employeeRepository;
        _headRepository = headRepository;
        _leaderRepository = leaderRepository;
    }

    public async Task CreateAsync(
        string name,
        [CanBeNull] string? description,
        [NotNull] DateTime startTime,
        [NotNull] Double salary,
        [NotNull] EmployeeDepartment department,
        [CanBeNull] string? headName,
        [CanBeNull] string[]? headNames)
    {
        var employee = new Employee(GuidGenerator.Create(), name, department, salary, startTime, headName, description);
        var count = await GetCountAsync(name);
        if (count > 0)
        {
            employee.SetName(name + "(" + count.ToString() + ")");
            await _headRepository.InsertAsync(new Head(GuidGenerator.Create(), employee.Name)); 
            await _employeeRepository.InsertAsync(employee);
        }
        else
        {
            await _headRepository.InsertAsync(new Head(GuidGenerator.Create(), employee.Name));
            await _employeeRepository.InsertAsync(employee);
        }
        
        await SetHeadsAsync(employee, headNames);
        await SetLeadersAsync(employee.Name, "", true);
        await _employeeRepository.InsertAsync(employee);
        
    }

    public async Task UpdateAsync(
        Employee employee,
        string name,
        [CanBeNull] string? description,
        [NotNull] DateTime startTime,
        [NotNull] double salary,
        [NotNull] EmployeeDepartment department,
        [CanBeNull] string? headName = null,   
        [CanBeNull] string []? headNames =null
    )
    {
        string namecp = employee.Name;
        employee.SetName(name);
        employee.Description = description;
        employee.StartTime = startTime;
        employee.Salary = salary;
        employee.Department = department;
        employee.HeadName = headName;

        var count = await GetCountAsync(name);
        if (count > 0)
        {
            employee.SetName(name+"("+count.ToString()+")");
            var head = await _headRepository.GetAsync(x => x.Name == namecp);
            head.SetName(employee.Name);
            await _headRepository.UpdateAsync(head);
            await _employeeRepository.UpdateAsync(employee);
        }
        else
        {
            var head = await _headRepository.GetAsync(x => x.Name == namecp);
            head.SetName(employee.Name);
            await _headRepository.UpdateAsync(head);
            await _employeeRepository.UpdateAsync(employee);
        }
        await SetHeadsAsync(employee, headNames);
        await SetLeadersAsync(employee.Name,namecp, false);
        
        
    }
    private async Task<int> GetCountAsync(string name)
    {

        var res = await _employeeRepository.GetNamesCountAsync(name);
        return res;
    }
    private async Task SetLeadersAsync([NotNull] string name, string oldName, bool isCreated)
    {
        

        var query2 = (await _leaderRepository.GetQueryableAsync())
            .Where(x => x.Name.Equals(oldName))
            .Select(x => x.Id)
            .Distinct();
        var leaderIds = await AsyncExecuter.ToListAsync(query2);
        if(leaderIds.Any() || !leaderIds.IsNullOrEmpty()) { 
            await _leaderRepository.DeleteAsync(x=> x.Id==leaderIds[0]);
        await _leaderRepository.InsertAsync(new Leader(leaderIds[0], name));
        }
        else
        {

        await _leaderRepository.InsertAsync(new Leader(GuidGenerator.Create(), name));
        }
    }
    private async Task SetHeadsAsync(Employee employee, [CanBeNull] string[]? headNames)
    {

        if (headNames == null || !headNames.Any())
        {
            employee.RemoveAllHeads();
            return;
        }

        var query = (await _headRepository.GetQueryableAsync())
            .Where(x => headNames.Contains(x.Name))
            .Select(x => x.Id)
            .Distinct();

        var headIds = await AsyncExecuter.ToListAsync(query);
        if (!headIds.Any())
        {
            return;
        }

        employee.RemoveAllHeadsExceptGivenIds(headIds);

        foreach (var headId in headIds)
        {
            employee.AddHead(headId);
        }
    }
}
