using EmployeeTracker.HeadLists;
using EmployeeTracker.Heads;
using EmployeeTracker.LeaderLists;
using EmployeeTracker.Leaders;
using EmployeeTracker.Projects;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace EmployeeTracker.Employees
{
    public class EmployeeAppService : EmployeeTrackerAppService, IEmployeeAppService
    {
        private readonly IProjectRepository _projectRepository; 
        private readonly IEmployeeRepository _employeeRepository;
        private readonly EmployeeManager _employeeManager;
        private readonly IRepository<Head, Guid> _headRepository;
        private readonly IRepository<Leader, Guid> _leaderRepository;
        private readonly IRepository <LeaderList> _leaderListRepository;
        private readonly IRepository<HeadList> _headListRepository;
        private readonly ProjectManager _projectManager;
        public EmployeeAppService(IEmployeeRepository employeeRepository,
            EmployeeManager employeeManager, 
            IRepository<Head, Guid> headRepository,
            IProjectRepository projectRepository,
            IRepository<Leader, Guid> leaderRepository,
            IRepository<LeaderList> leaderListRepository,
            IRepository<HeadList> headListRepository,
            ProjectManager projectManager
           )
        {
            _employeeRepository = employeeRepository;
            _employeeManager = employeeManager;
            _headRepository = headRepository;
            _leaderRepository = leaderRepository;
            _headListRepository = headListRepository;
            _leaderListRepository = leaderListRepository;
            _projectManager = projectManager;
            _projectRepository = projectRepository;
        }

        public async Task CreateAsync(CreateEmployeeDto input)
        {
            await _employeeManager.CreateAsync(
                input.Name,
                input.Description,
                (DateTime)input.StartTime,
                input.Salary,
                input.Department,
                input.HeadName,
                input.HeadNames
                );
        }

        public async Task DeleteAsync(Guid id)
        {
            Employee employee = await _employeeRepository.GetAsync( id , includeDetails:true);
            
            await _employeeRepository.DeleteAsync(id);
           
            await _headRepository.DeleteAsync(x => x.Name == employee.Name);
            await _leaderRepository.DeleteAsync(x => x.Name == employee.Name);

            var emps = await _employeeRepository.GetListAsync();
            for(var x = 0; x < emps.Count;x++)
            {
                emps[x].HeadName = emps[x].HeadName == employee.Name ? null : emps[x].HeadName;
                await _employeeRepository.UpdateAsync(emps[x]);
            }

        }

        public async Task<EmployeeDto> GetAsync(Guid id)
        {
            var employee = await _employeeRepository.GetAsync(id);

            return ObjectMapper.Map<EmployeeWithDetails, EmployeeDto>(employee);
        }

        public async Task<ListResultDto<HeadLookupDto>> GetHeadLookupAsync()
        {
            var heads = await _headRepository.GetListAsync();

            return new ListResultDto<HeadLookupDto>(
                    ObjectMapper.Map<List<Head>, List<HeadLookupDto>>(heads));
        }

        public async Task<ListResultDto<LeaderLookupDto>> GetLeaderLookupAsync()
        {
            var leaders = await _leaderRepository.GetListAsync();

            return new ListResultDto<LeaderLookupDto>(
                    ObjectMapper.Map<List<Leader>, List<LeaderLookupDto>>(leaders));
        }

        public async Task<ListResultDto<ProjectLookupDto>> GetProjectLookupAsync()
        {
            var projects = await _projectRepository.GetListAsync();

            return new ListResultDto<ProjectLookupDto>(
                    ObjectMapper.Map<List<Project>, List<ProjectLookupDto>>(projects));
        }
        public async Task<PagedResultDto<EmployeeDto>> GetListAsync(EmployeeGetListInput input)
        {
            var employees = await _employeeRepository.GetListAsync(
                input.Sorting,
                input.SkipCount,
                input.MaxResultCount);

            var totalCount = await _employeeRepository.CountAsync();

            return new PagedResultDto<EmployeeDto>
                (totalCount, ObjectMapper.Map<List<EmployeeWithDetails>, List<EmployeeDto>>(employees));
        }

        public async Task UpdateAsync(Guid id, UpdateEmployeeDto input)
        {
            var employee = await _employeeRepository.GetAsync(id, includeDetails: true);

            await _employeeManager.UpdateAsync(
                employee,
                input.Name,
                input.Description,
                (DateTime)input.StartTime,
                input.Salary,
                input.Department,
                input.HeadName,
                input.HeadNames);
            
        }
        
        

    }
}
