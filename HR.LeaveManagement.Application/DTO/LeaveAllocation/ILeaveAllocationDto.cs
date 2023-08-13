using HR.LeaveManagement.Application.DTO.LeaveType;

namespace HR.LeaveManagement.Application.DTO.LeaveAllocation
{
    public interface ILeaveAllocationDto
    {
        DateTime Created { get; set; }
        LeaveTypeDto? LeaveType { get; set; }
        int LeaveTypeID { get; set; }
        int NumberOfDays { get; set; }
        int Period { get; set; }
    }
}