using HR.LeaveManagement.Application.DTO.LeaveType;

namespace HR.LeaveManagement.Application.DTO.LeaveRequest
{
    public interface ILeaveRequestDto
    {
        bool Approved { get; set; }
        bool Cancelled { get; set; }
        DateTime DateActioned { get; set; }
        DateTime DateRequested { get; set; }
        DateTime EndDate { get; set; }
        LeaveTypeDto LeaveType { get; set; }
        int LeaveTypeId { get; set; }
        string? RequestComments { get; set; }
        DateTime StartDate { get; set; }
    }
}