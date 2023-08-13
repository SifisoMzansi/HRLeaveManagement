using AutoMapper;
using HR.LeaveManagement.Application.DTO.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Command;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using MediatR;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Models;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handler.Command;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;

    public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper , IEmailSender emailSender)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        _emailSender = emailSender;
}
    public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new CreateLeaveRequestDtoValidator(_leaveRequestRepository);

        var validationResult = await validator.ValidateAsync(request.LeaveRequest!);
        if(!validationResult.IsValid) 
        {
            response.Success = false;
            response.Message = "Creation Failed";
            response.Errors = validationResult.Errors.Select(err => err.ErrorMessage).ToList() ;

           // throw new ValidationException(validationResult);
        }
        
        var LeaveRequest  =  _mapper.Map<LeaveRequest>(request.LeaveRequest);
         await _leaveRequestRepository.Add(LeaveRequest);

        response.Success = true;
        response.Message = "Creation Successful";
        response.Id = LeaveRequest.Id;

        var email = new Email
        {
            To = "admin456sfiso@gmail.com",
            EmailBody = $"Your leave request for {request.LeaveRequest!.StartDate!} to {request.LeaveRequest.EndDate} " +
             $" has been submitted successfully",
            Subject = "Leave request submitted"
        };

        try
        { 
            await _emailSender.SendEmailAsync(email);
        }
        catch (Exception ex)
        {

        }



        return response;
    }
}
