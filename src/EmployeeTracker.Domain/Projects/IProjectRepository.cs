using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;

namespace EmployeeTracker.Projects;

public interface IProjectRepository : IRepository<Project, Guid>
{
    Task<List<ProjectWithDetails>> GetListAsync(
        string sorting, int skipCount, int maxResultCount, CancellationToken cancellationToken = default
        );
    Task<ProjectWithDetails> GetAsync(Guid id, CancellationToken cancellationToken=default);
}
