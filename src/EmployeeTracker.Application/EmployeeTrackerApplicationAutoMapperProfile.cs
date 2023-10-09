using AutoMapper;
using EmployeeTracker.Employees;
using EmployeeTracker.Leaders;
using EmployeeTracker.Projects;
using EmployeeTracker.Heads;

namespace EmployeeTracker;

public class EmployeeTrackerApplicationAutoMapperProfile : Profile
{
    public EmployeeTrackerApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Leader, LeaderDto>();
        CreateMap<Leader, LeaderLookupDto>();
        CreateMap<CreateLeaderDto, Leader>();
        CreateMap<UpdateLeaderDto, Leader>();
        CreateMap<Head, HeadDto>();
        CreateMap<Head, HeadLookupDto>();
        CreateMap<CreateHeadDto, Head>();
        CreateMap<UpdateHeadDto, Head>();


        CreateMap<EmployeeWithDetails, EmployeeDto>();
        CreateMap<ProjectWithDetails, ProjectDto>();
    }
}
