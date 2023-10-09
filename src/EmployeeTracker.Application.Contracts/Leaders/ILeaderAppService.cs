﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EmployeeTracker.Leaders
{
    public interface ILeaderAppService
        : ICrudAppService<LeaderDto, Guid, PagedAndSortedResultRequestDto, CreateLeaderDto, UpdateLeaderDto>
    {

    }
}
