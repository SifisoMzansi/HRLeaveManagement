namespace HR.LeaveManagement.Application.DTO.LeaveType
{
    public interface ILeaveTypeDto
    {
        int DefaultDays { get; set; }
        string? Name { get; set; }
    }
}