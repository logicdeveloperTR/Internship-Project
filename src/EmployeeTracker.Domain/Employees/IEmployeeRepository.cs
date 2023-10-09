using EmployeeTracker.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EmployeeTracker.Employees;

public interface IEmployeeRepository : IRepository<Employee, Guid>
{
    Task<int> GetNamesCountAsync(string name, CancellationToken cancellationToken=default);
    Task<List<EmployeeWithDetails>> GetListAsync(
        string sorting, int skipCount, int maxResultCount, CancellationToken cancellationToken = default
        );
    Task<EmployeeWithDetails> GetAsync(Guid id, CancellationToken cancellationToken=default);
}
