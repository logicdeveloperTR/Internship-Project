using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace EmployeeTracker.HeadLists;

public class HeadList:Entity
{
    public Guid HeadId { get; protected set; }    

    public Guid EmployeeId { get; protected set; } 

    private HeadList()
    {

    }

    public HeadList(Guid headId, Guid employeeId)
    {
        HeadId = headId;
        EmployeeId = employeeId;
    }

    public override object[] GetKeys() {
        return new object[]{EmployeeId, HeadId};
    }
}
