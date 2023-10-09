using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EmployeeTracker.Leaders;

public class LeaderLookupDto : EntityDto<Guid>
{
    public string Name { get; set; }    
}
