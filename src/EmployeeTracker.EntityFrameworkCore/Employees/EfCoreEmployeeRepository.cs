using EmployeeTracker.Leaders;
using EmployeeTracker.EntityFrameworkCore;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using EmployeeTracker.Heads;
using System.Linq.Dynamic.Core;

namespace EmployeeTracker.Employees;

public class EfCoreEmployeeRepository : EfCoreRepository<EmployeeTrackerDbContext, Employee, Guid>,  IEmployeeRepository
{
    public EfCoreEmployeeRepository(IDbContextProvider<EmployeeTrackerDbContext> dbContextProvider):
        base(dbContextProvider)
    {
    }

    public async Task<EmployeeWithDetails> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var query = await ApplyFilterAsync();

        EmployeeWithDetails employeeWithDetails = await query
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        return employeeWithDetails;
    }
    public async Task<int> GetNamesCountAsync(string name, CancellationToken cancellationToken=default)
    {
        var x = await GetCountQueryAsync();
        var res = await x.Where(x => x.Name == name)
            .OrderBy(x => x.Name)
            .ToListAsync(GetCancellationToken(cancellationToken));
        return res.Count;
    }
    private async Task<IQueryable<EmployeeWithDetails>> ApplyFilterAsync()
    {
        var dbContext = await GetDbContextAsync();

        return (await GetDbSetAsync())
            .Include(x => x.HeadNames)
            .Select(x => new EmployeeWithDetails
            {
                Id=x.Id,
                Name = x.Name,
                Salary = x.Salary,
                StartTime = x.StartTime,
                Description = x.Description,
                Department = x.Department,
                HeadName = x.HeadName,
                HeadNames = (from headNames in x.HeadNames
                             join head in dbContext.Set<Head>() on headNames.HeadId equals head.Id
                             select head.Name).ToArray()
            });
    }
    private async Task<IQueryable<EmployeeWithDetails>> GetCountQueryAsync()
    {
        var dbContext = await GetDbContextAsync();

        return (await GetDbSetAsync())
            .Include(x => x.HeadNames)
            .Select(x => new EmployeeWithDetails
            {
               
                Name = x.Name,
            });
    }

   
    public async Task<List<EmployeeWithDetails>> GetListAsync(string sorting,
        int skipCount, int maxResultCount, CancellationToken cancellationToken= default) { 
        
        var query = await ApplyFilterAsync();
        return await query.
            OrderBy(x=>x.Name)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    
    public override Task<IQueryable<Employee>> WithDetailsAsync()
    {
        return base.WithDetailsAsync(x => x.HeadNames);
    }
}
