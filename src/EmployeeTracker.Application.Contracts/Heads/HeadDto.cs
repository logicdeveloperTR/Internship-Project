﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EmployeeTracker.Heads;

public class HeadDto : EntityDto<Guid>
{
    public string Name { get; set; }
}
