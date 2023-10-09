using EmployeeTracker.Leaders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EmployeeTracker.Projects;

public interface IProjectAppService:IApplicationService
{
    Task<PagedResultDto<ProjectDto>> GetListAsync(ProjectGetListInput input);

    Task<ProjectDto> GetAsync(Guid id);

    Task CreateAsync(CreateProjectDto input);

    Task UpdateAsync(Guid id, UpdateProjectDto input);

    Task DeleteAsync(Guid id);
    Task<ListResultDto<LeaderLookupDto>> GetLeaderLookupAsync();

}
