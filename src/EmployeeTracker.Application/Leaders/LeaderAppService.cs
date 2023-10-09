using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace EmployeeTracker.Leaders
{
    public class LeaderAppService
        : CrudAppService<Leader,
            LeaderDto, 
            Guid,
            PagedAndSortedResultRequestDto,
            CreateLeaderDto,
            UpdateLeaderDto>
        , ILeaderAppService
    {
        public LeaderAppService(IRepository<Leader, Guid> repository):base(repository)
        {

        }
    }
}
