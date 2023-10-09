using EmployeeTracker.Leaders;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace EmployeeTracker.Projects;

public class ProjectManager : DomainService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IRepository<Leader, Guid> _leaderRepository;

    public ProjectManager(IProjectRepository projectRepository, IRepository<Leader, Guid> leaderRepository)
    {
        _projectRepository = projectRepository;
        _leaderRepository = leaderRepository;
    }

    public async Task CreateAsync(
        string name,
        [NotNull] DateTime startTime, 
        [NotNull] DateTime endTime,
        [CanBeNull] string[]? leaderNames=null,
        [CanBeNull] string? description=null)
    {
        var project = new Project(GuidGenerator.Create(), name,  startTime, endTime,description);

        await SetLeadersAsync(project, leaderNames);
        
        await _projectRepository.InsertAsync(project);
    }

    public async Task UpdateAsync(
        Project project,
        [NotNull] string name,
        
        [NotNull] DateTime startTime,
        [NotNull] DateTime endTime,
        [CanBeNull] string? description = null,
        [CanBeNull] string[]? leaderNames = null
    )
    {
        
        project.SetName(name);
        project.Description = description;
        project.StartTime = startTime;
        project.EndTime = endTime;
        await SetLeadersAsync(project, leaderNames);

        await _projectRepository.UpdateAsync(project);
    }

    private async Task SetLeadersAsync(Project project, [CanBeNull] string[]? leaderNames)
    {
        if (leaderNames == null || !leaderNames.Any())
        {
            project.RemoveAllLeaders();
            return;
        }

        var query = (await _leaderRepository.GetQueryableAsync())
            .Where(x => leaderNames.Contains(x.Name))
            .Select(x => x.Id)
            .Distinct();

        var leaderIds = await AsyncExecuter.ToListAsync(query);
        if (!leaderIds.Any())
        {
            return;
        }

        project.RemoveAllLeadersExceptGivenIds(leaderIds);

        foreach (var leaderId in leaderIds)
        {
            project.AddLeader(leaderId);
        }
    }
}
