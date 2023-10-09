using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace EmployeeTracker.LeaderLists;


public class LeaderList : Entity
{
    public Guid ProjectId { get; protected set; }

    public Guid LeaderId { get; protected set; }

    private LeaderList()
    {

    }

    public LeaderList(Guid projectId, Guid leaderId)
    {
        ProjectId = projectId;
        LeaderId = leaderId;
    }
    public override object[] GetKeys()
    {
        return new object[] { ProjectId, LeaderId };

    }
}
