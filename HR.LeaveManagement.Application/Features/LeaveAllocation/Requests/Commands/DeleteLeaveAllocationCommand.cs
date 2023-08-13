using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Requests.Commands
{
    public class DeleteLeaveAllocationCommand :IRequest<Unit>
    {

        public int Id { get; set; }
    }
}
