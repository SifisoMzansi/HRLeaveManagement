using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTO.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Features.LeaveRequests.Requests.Command;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;


namespace HR.LeaveManagement.Application.Features.LeaveRequests.Handler.Command;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
{
    //private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper, IEmailSender emailSender, IHttpContextAccessor httpContextAccessor,   IUnitOfWork unitOfWork)
    {
      //  _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        _emailSender = emailSender;
        _httpContextAccessor = httpContextAccessor;
      //  _leaveAllocationRepository = leaveAllocationRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new CreateLeaveRequestDtoValidator(_unitOfWork.LeaveRequestRepository);

        var validationResult = await validator.ValidateAsync(request.LeaveRequestDto!);
        var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(q => q.Type=="uid")?.Value;
        var allocation = await _leaveAllocationRepository.GetUserAllocations(userId, request.LeaveRequestDto!.LeaveTypeId);
        int daysRequested = (int) (request.LeaveRequestDto!.EndDate -  request.LeaveRequestDto!.StartDate).TotalDays;

        if(daysRequested > allocation.NumberOfDays) 
        {
            validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
                nameof(request.LeaveRequestDto.EndDate),"You do not have enough days for this request"));
        }

        if (!validationResult.IsValid)
        {
            response.Success = false;
            response.Message = "Creation Failed";
            response.Errors = validationResult.Errors.Select(err => err.ErrorMessage).ToList();
        }
        
        var LeaveRequest  =  _mapper.Map<LeaveRequest>(request.LeaveRequestDto);
        LeaveRequest.RequestingEmployeeId = userId!;
        await _unitOfWork.LeaveRequestRepository.Add(LeaveRequest);
        await _unitOfWork.Save();

        response.Success = true;
        response.Message = "Leave request Creation Successful";
        response.Id = LeaveRequest.Id;

        var emailAddress = _httpContextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Email)!.Value;
        var email = new Email
        {
            To = emailAddress,
            EmailBody = $"Your leave request for {request.LeaveRequestDto!.StartDate!} to {request.LeaveRequestDto.EndDate} " +
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
