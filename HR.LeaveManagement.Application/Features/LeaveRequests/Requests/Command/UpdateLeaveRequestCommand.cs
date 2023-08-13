using HR.LeaveManagement.Application.DTO.LeaveRequest;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Command
{
    public class UpdateLeaveRequestCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public UpdateLeaveRequestDto LeaveRequestDto { get; set; } = null!;
        public CreateLeaveRequestDto CreateLeaveRequestDto { get; set; } = null!;
        public ChangeLeaveRequestApprovalDto ChangeLeaveRequestApprovalDto { get; set; } = null!;
        
    }
}
