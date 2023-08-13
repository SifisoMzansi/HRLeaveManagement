using AutoMapper;
using HR.LeaveManagement.Application.DTO.LeaveType.Validators;
using HR.LeaveManagement.Application.Features.LeaveType.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Features.LeaveType.Handler.Commands
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {

        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository , IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {

            var leaveTypeValidation = new UpdateLeaveTypeDtoValidator();
            var validationResult = leaveTypeValidation.Validate(request.LeaveTypeDto);

            if (!validationResult.IsValid)
            {
                throw new Exceptions.ValidationException(validationResult);
            }

            var Exists = await _leaveTypeRepository.Get(request.LeaveTypeDto.Id);

            if (Exists != null)
            {
                var leaveType = new Domain.LeaveType();
                _mapper.Map(request.LeaveTypeDto, leaveType); 
                await _leaveTypeRepository.Update(leaveType);
            }
                      
            return Unit.Value;
        }
    }
}
