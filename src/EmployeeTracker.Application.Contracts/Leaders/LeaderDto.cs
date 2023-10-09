using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EmployeeTracker.Leaders;

public class LeaderDto : EntityDto<Guid>
{
    public string Name { get; set; }
}
