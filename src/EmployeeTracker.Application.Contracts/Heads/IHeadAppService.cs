using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EmployeeTracker.Heads
{
    public interface IHeadAppService
        : ICrudAppService<HeadDto, Guid, PagedAndSortedResultRequestDto, CreateHeadDto, UpdateHeadDto>
    {

    }
}
