using HR.LeaveManagement.Application.DTO.Common;
using HR.LeaveManagement.Application.DTO.LeaveType;

namespace HR.LeaveManagement.Application.DTO.LeaveAllocation;
public class LeaveAllocationDto : BaseDto, ILeaveAllocationDto
{
    public int NumberOfDays { get; set; }
    public DateTime Created { get; set; }
    public LeaveTypeDto? LeaveType { get; set; }
    public int LeaveTypeID { get; set; }
    public int Period { get; set; }
}