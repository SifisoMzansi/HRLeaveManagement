namespace HR.LeaveManagement.Application.DTO.LeaveAllocation
{
    public interface ICreateLeaveAllocationDto
    {
        int LeaveTypeID { get; set; }
        int NumberOfDays { get; set; }
        int Period { get; set; }
    }
}