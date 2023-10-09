using AutoMapper;
using EmployeeTracker.Employees;
using EmployeeTracker.Heads;
using EmployeeTracker.Leaders;
using EmployeeTracker.Projects;
using EmployeeTracker.Web.Models;
using Volo.Abp.AutoMapper;

namespace EmployeeTracker.Web;

public class EmployeeTrackerWebAutoMapperProfile : Profile
{
    public EmployeeTrackerWebAutoMapperProfile()
    {
        
        //Define your AutoMapper configuration here for the Web project.
        CreateMap<LeaderLookupDto, LeaderViewModel>()
               .Ignore(x => x.IsSelected);

        CreateMap<HeadLookupDto, HeadViewModel>()
               .Ignore(x => x.IsSelected);

        CreateMap<EmployeeDto, CreateEmployeeDto>();

        CreateMap<EmployeeDto, UpdateEmployeeDto>();

        CreateMap<ProjectDto, CreateProjectDto>();

        CreateMap<ProjectDto, UpdateProjectDto>();

        CreateMap<HeadDto, CreateHeadDto>();

        CreateMap<HeadDto, UpdateHeadDto>();

        CreateMap<LeaderDto, CreateLeaderDto>();



        CreateMap<LeaderDto, UpdateLeaderDto>();
    }
}
