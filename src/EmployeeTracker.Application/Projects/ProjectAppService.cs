using EmployeeTracker.Leaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace EmployeeTracker.Projects
{
    public class ProjectAppService : EmployeeTrackerAppService, IProjectAppService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ProjectManager _projectManager;
        private readonly IRepository<Leader, Guid> _leaderRepository;

        public ProjectAppService(IProjectRepository projectRepository, ProjectManager projectManager, 
            IRepository<Leader, Guid> leaderRepository)
        {
            _projectRepository = projectRepository;
            _projectManager = projectManager;
            _leaderRepository = leaderRepository;
        }

        public async Task CreateAsync(CreateProjectDto input)
        {
            await _projectManager.CreateAsync(
                input.Name,
                
                input.StartTime,
                input.EndTime,
                input.LeaderNames,
                input.Description
                );
        }

        public async Task DeleteAsync(Guid id)
        {
            await _projectRepository.DeleteAsync(id);
        }


        public async Task<ProjectDto> GetAsync(Guid id)
        {
            var project = await _projectRepository.GetAsync(id);

            return ObjectMapper.Map<ProjectWithDetails, ProjectDto>(project);
        }

        public async Task<ListResultDto<LeaderLookupDto>> GetLeaderLookupAsync()
        {
            var leaders = await _leaderRepository.GetListAsync();

            return new ListResultDto<LeaderLookupDto>(
                    ObjectMapper.Map<List<Leader>, List<LeaderLookupDto>>(leaders));
        }

        public async Task<PagedResultDto<ProjectDto>> GetListAsync(ProjectGetListInput input)
        {
            var projects = await _projectRepository.GetListAsync(
                input.Sorting,
                input.SkipCount,
                input.MaxResultCount);

            var totalCount = await _projectRepository.CountAsync();

            return new PagedResultDto<ProjectDto>
                (totalCount, ObjectMapper.Map<List<ProjectWithDetails>, List<ProjectDto>>(projects));
        }

        public async Task UpdateAsync(Guid id, UpdateProjectDto input)
        {
            var project = await _projectRepository.GetAsync(id, includeDetails: true);

            await _projectManager.UpdateAsync(
                project,
                input.Name,
                input.StartTime,
                input.EndTime,
                input.Description,
                input.LeaderNames
                );


        }


    }
}
