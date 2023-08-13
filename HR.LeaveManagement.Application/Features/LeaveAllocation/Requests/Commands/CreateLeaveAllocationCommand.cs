using HR.LeaveManagement.Application.DTO.LeaveAllocation;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Requests.Commands;

public class CreateLeaveAllocationCommand : IRequest<Unit>
{
    public CreateLeaveAllocationDto LeaveAllocationDto { get; set; } 
}
