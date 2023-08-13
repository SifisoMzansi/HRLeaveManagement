using HR.LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Domain
{
     [Owned]
    [PrimaryKey("Id")]

    public class LeaveType : BaseDomainEntity
    {
        public int Id { get; set; }

        public string? Name { get; set; }    
        public int DefaultDays { get; set; }
     }
}
