namespace HR.LeaveManagement.Application.DTO.LeaveType
{
    public interface ICreateLeaveTypeDto
    {
        int DefaultDays { get; set; }
        string? Name { get; set; }
    }
}