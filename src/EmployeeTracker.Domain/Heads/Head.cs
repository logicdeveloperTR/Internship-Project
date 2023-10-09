using EmployeeTracker.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace EmployeeTracker.Heads;

public class Head : AuditedAggregateRoot<Guid>
{
    public string Name { get; private set; }
    private Head()
    {

    }

   
    public Head(Guid id, string name) : base(id)
    {
        Name = name;
    }
    public Head SetName(string name)
    {

        Name = Check.NotNullOrWhiteSpace(name, nameof(name), ProjectConsts.MaxStr);
        return this;
    }

}
