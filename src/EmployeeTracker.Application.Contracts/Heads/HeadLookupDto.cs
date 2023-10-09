using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EmployeeTracker.Heads;

public class HeadLookupDto : EntityDto<Guid>
{
    public string Name { get; set; }    
}
