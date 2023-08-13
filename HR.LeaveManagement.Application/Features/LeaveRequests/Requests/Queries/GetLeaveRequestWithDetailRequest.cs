using HR.LeaveManagement.Application.DTO.LeaveRequest;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;

public class GetLeaveRequestWithDetailRequest : IRequest<LeaveRequestDto>
{
    public int Id { get; set; }
}
