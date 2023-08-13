using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Command;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handler.Command
{
    public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
        }

        public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var LeaveRequest = _leaveRequestRepository.Get(request.Id);
            if (LeaveRequest is null)
            {
                throw new NotFoundException(nameof(Domain.LeaveRequest), request.Id);
            }
            await _leaveRequestRepository.Delete(LeaveRequest.Result);
            return Unit.Value;
        }

        

        //public async Task Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        //{

        //    var LeaveRequest = _leaveRequestRepository.Get(request.Id);
        //    if (LeaveRequest is null)
        //    {
        //        throw new NotFoundException(nameof(Domain.LeaveRequest) , request.Id);
        //    }
        //    await _leaveRequestRepository.Delete(LeaveRequest.Result);
        //}


    }
}
