namespace HR.LeaveManagement.Application.DTO.LeaveType
{
    public class CreateLeaveTypeDto : ICreateLeaveTypeDto
    {
        public string? Name { get; set; }
        public int DefaultDays { get; set; }
    }
}
