namespace HR.LeaveManagement.Application.DTO.LeaveRequest
{
    public interface ICreateLeaveRequestDto
    {
        DateTime EndDate { get; set; }
        int LeaveTypeId { get; set; }
        string RequestComments { get; set; }
        DateTime StartDate { get; set; }
    }
}