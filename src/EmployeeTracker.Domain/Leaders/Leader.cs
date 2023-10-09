using EmployeeTracker.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace EmployeeTracker.Leaders;

public class Leader: AuditedAggregateRoot<Guid>
{
    public string Name{get; private set;}
    private Leader()
    {

    }

    
    public Leader(Guid id, string name): base(id)
    {
        Name = name;
    }
    public Leader SetName(string name) {
        
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), ProjectConsts.MaxStr);
        return this; 

    }
}
