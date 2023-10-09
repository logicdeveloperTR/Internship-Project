using EmployeeTracker.Leaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace EmployeeTracker.Heads
{
    public class HeadAppService
        : CrudAppService<Head, HeadDto, Guid, PagedAndSortedResultRequestDto, CreateHeadDto, UpdateHeadDto>
        , IHeadAppService
    {
        public HeadAppService(IRepository<Head, Guid> repository):base(repository)
        {

        }
    }
}
