using EmployeeTracker.LeaderLists;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace EmployeeTracker.Projects;
public class Project : FullAuditedAggregateRoot<Guid>
{
    public string Name { get; private set; }

    [CanBeNull]
    public string? Description { get; set; }

    public Collection<LeaderList> LeaderNames { get; private set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    private Project()
    {

    }

    public Project(Guid id,
        [NotNull] string name, 
        
        [NotNull] DateTime startTime,
        [NotNull] DateTime endTime,
        [CanBeNull] string? description=null
        ):base(id)
        
    {
        SetName(name);
        Description = description;
        StartTime = startTime;
        EndTime = endTime;
        LeaderNames = new Collection<LeaderList>();
    }

    public void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), ProjectConsts.MaxStr);    
    }

    public void AddLeader(Guid leaderId)
    {
        Check.NotNull(leaderId, nameof(leaderId));

        if (IsInLeaders(leaderId))
        {
            return;
        }
        LeaderNames.Add(new LeaderList(leaderId:leaderId, projectId:Id));
    }

    public void RemoveLeader(Guid leaderId) {

        Check.NotNull(leaderId, nameof(leaderId)); 
        if (!IsInLeaders(leaderId)) 
        { 
            return;
        }
        LeaderNames.RemoveAll(x => x.LeaderId == leaderId);
    }

    public void RemoveAllLeadersExceptGivenIds(List<Guid> leaderIds) 
    { 
        Check.NotNullOrEmpty(leaderIds, nameof(leaderIds));
        LeaderNames.RemoveAll(x => !leaderIds.Contains(x.LeaderId));
    }
    public void RemoveAllLeaders() { 
        LeaderNames.RemoveAll(x => x.ProjectId == Id); 
    }
    private bool IsInLeaders(Guid leaderId) 
    { 
        return LeaderNames.Any(x => x.LeaderId == leaderId);
    }
}
