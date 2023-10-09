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

namespace EmployeeTracker.Projects;

public class EfCoreProjectRepository : EfCoreRepository<EmployeeTrackerDbContext, Project, Guid>, IProjectRepository
{
    public EfCoreProjectRepository(IDbContextProvider<EmployeeTrackerDbContext> dbContextProvider) :
        base(dbContextProvider)
    {
    }

    public async Task<ProjectWithDetails> GetAsync(Guid id, 
        CancellationToken cancellationToken=default)
    {
        var query = await ApplyFilterAsync();

        ProjectWithDetails projectWithDetails = await query
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        return projectWithDetails;
    }
    private async Task<IQueryable<ProjectWithDetails>> ApplyFilterAsync()
    {
        var dbContext = await GetDbContextAsync();

        return (await GetDbSetAsync())
            .Include(x => x.LeaderNames)
            .Select(x => new ProjectWithDetails
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                LeaderNames = (from leaderNames in x.LeaderNames
                             join leader in dbContext.Set<Leader>() on leaderNames.LeaderId equals leader.Id
                             select leader.Name).ToArray()
            });
    }
    public async Task<List<ProjectWithDetails>> GetListAsync(string sorting,
        int skipCount, int maxResultCount, CancellationToken cancellationToken = default)
    {

        var query = await ApplyFilterAsync();
        return await query.
            OrderBy(x => x.Name)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }


    public override Task<IQueryable<Project>> WithDetailsAsync()
    {
        return base.WithDetailsAsync(x => x.LeaderNames);
    }
}
