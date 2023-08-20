using HR.LeaveManagement.Application.DTO.LeaveAllocation;
using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Requests.Commands;

public class CreateLeaveAllocationCommand : IRequest<BaseCommandResponse>
{
    public CreateLeaveAllocationDto LeaveAllocationDto { get; set; } 
}
