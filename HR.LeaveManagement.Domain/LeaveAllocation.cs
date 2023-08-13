using HR.LeaveManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Domain
{
    public class LeaveAllocation : BaseDomainEntity
    {
         public  int NumberOfDays { get; set; }
        public  DateTime Created { get; set; }
         
       // public LeaveType? LeaveType { get; set; } 
        public  int  LeaveTypeID { get; set; }
        public int  Period { get; set; }
    }
}
