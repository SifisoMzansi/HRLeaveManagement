using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTO.LeaveType.Validators;
using HR.LeaveManagement.Application.Features.LeaveType.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HR.LeaveManagement.Application.Features.LeaveType.Handler.Commands;

public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, BaseCommandResponse>
{
    private readonly ILeaveTypeRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveRequestRepository, IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var leaveTypeValidator = new CreateLeaveTypeDtoValidator();
        var validationResult = await leaveTypeValidator.ValidateAsync(request.LeaveTypeDto);

        if (!validationResult.IsValid)
        {
            response.Success = false;
            response.Message = "Creation Failed";
             foreach (var error in validationResult.Errors)
            {
                response.Errors.Add(error.ErrorMessage);

            }
        }
        else
        {
            var leaveType = _mapper.Map<Domain.LeaveType>(request.LeaveTypeDto);
            await _leaveRequestRepository.Add(leaveType);
            response.Success = true;
         
            response.Message = "Creation Successful";
        }
        return response;

    }
}