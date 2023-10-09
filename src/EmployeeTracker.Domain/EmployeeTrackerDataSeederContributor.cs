using System;
using System.Threading.Tasks;
using EmployeeTracker.Employees;
using EmployeeTracker.Heads;
using EmployeeTracker.Projects;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace EmployeeTracker
{
    public class EmployeeTrackerDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly EmployeeManager _employeeManager;
        private readonly ProjectManager _projectManager;    
        private readonly IRepository<Employee, Guid> _employeeRepository;
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly IRepository<Head, Guid> _headRepository;
        public EmployeeTrackerDataSeederContributor(
            IGuidGenerator guidGenerator,
            EmployeeManager employeeManager,
            ProjectManager projectManager,
            IRepository<Employee, Guid> employeeRepository,
            IRepository<Project, Guid> projectRepository,
            IRepository<Head, Guid> headRepository
        )
        {
            _employeeManager = employeeManager;
            _guidGenerator = guidGenerator;
            _projectManager = projectManager;   
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;
            _headRepository = headRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if(await _headRepository.GetCountAsync() <= 0)
            {
               await _headRepository.InsertAsync(new Head(_guidGenerator.Create(), ""));
            }
            if (await _employeeRepository.GetCountAsync() <= 0)
            {
                await _employeeManager.CreateAsync(
                    
                    "Ozan",
                    null,
                    new DateTime(2023, 8, 28),
                    20000.0,
                    EmployeeDepartment.ArtificialIntelligence,
                    null,
                    null
                );

                await _employeeManager.CreateAsync(
                    "Hilmi",
                    null,
                    new DateTime(2023, 9, 23),
                    20000.0,
                    EmployeeDepartment.Law,
                    null,
                    headNames: null
                );
            }
            if (await _projectRepository.GetCountAsync() <= 0)
            {
                await _projectManager.CreateAsync(

                    "New Destiny",
                    
                    new DateTime(2023, 8, 28),
                    new DateTime(2023, 10, 28),
                    null,
                    null
                );

                await _projectManager.CreateAsync(

                    "Build New World",
                    new DateTime(2023, 11, 28),
                    new DateTime(2023, 12, 28),
                    null,
                    null
                );
            }
        }
    }   
 }