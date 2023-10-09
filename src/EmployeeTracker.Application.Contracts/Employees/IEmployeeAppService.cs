using EmployeeTracker.Heads;
using EmployeeTracker.Leaders;
using EmployeeTracker.Projects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EmployeeTracker.Employees;

public interface IEmployeeAppService:IApplicationService
{

    Task<PagedResultDto<EmployeeDto>> GetListAsync(EmployeeGetListInput input);

    Task<EmployeeDto> GetAsync(Guid id);

    Task CreateAsync(CreateEmployeeDto input);

    Task UpdateAsync(Guid id, UpdateEmployeeDto input);

    Task DeleteAsync(Guid id);
    Task<ListResultDto<HeadLookupDto>> GetHeadLookupAsync();

    Task<ListResultDto<ProjectLookupDto>> GetProjectLookupAsync();

    Task<ListResultDto<LeaderLookupDto>> GetLeaderLookupAsync();
}
