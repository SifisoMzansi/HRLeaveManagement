using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTO.LeaveAllocation.Validators;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using MediatR;
using domain = HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Handler.Commands;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, BaseCommandResponse>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;
    private readonly IUserService   _userService;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper, IUserService userService, ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
        _userService = userService;
        _leaveTypeRepository = leaveTypeRepository;
    }

   

    async Task<BaseCommandResponse> IRequestHandler<CreateLeaveAllocationCommand, BaseCommandResponse>.Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new CreateLeaveAllocationValidator();
        var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto);

        if (!validationResult.IsValid)
        {
            response.Success = false;
            response.Message = "Allocation Creation Failed";
            response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
        }
        else
        {
            var leaveType = await _leaveTypeRepository.Get(request.LeaveAllocationDto.LeaveTypeID);
            var employees = await _userService.GetEmployees();
            var period = DateTime.Now.Year;
            var allocations = new List<domain.LeaveAllocation>();
            foreach (var emp in employees)
            {
                if (await _leaveAllocationRepository.AllocationExists(emp.Id, leaveType.Id, period))
                    continue;

                allocations.Add(
                    new domain.LeaveAllocation
                     {
                         EmployeeId = emp.Id,
                         LeaveTypeID=leaveType.Id,
                         NumberOfDays=leaveType.DefaultDays,
                         Period=period
                    });
            }
            await _leaveAllocationRepository.AddAllocations(allocations);
            var leaveAllocation = _mapper.Map<domain.LeaveAllocation>(request.LeaveAllocationDto);

            response.Success = true;
            response.Message = "Allocation Successful";
           
        }

        return response;
    }



    //public async Task Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    //{
    //    var validator = new CreateLeaveAllocationValidator();
    //    var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto );

    //    if (!validationResult.IsValid)
    //    {
    //        throw new ValidationException.ValidationException(validationResult);
    //    }

    //    var leaveAllocation  = _mapper.Map<domain.LeaveAllocation>(request.LeaveAllocationDto);
    //    await _leaveAllocationRepository.Add(leaveAllocation);
    ////    return leaveAllocation.Id;
    //}

    //Task<Unit> IRequestHandler<CreateLeaveAllocationCommand, Unit>.Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    //{

    //    return Unit.Value;
    //}

    //public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    //{
    //    var validator = new CreateLeaveAllocationValidator();
    //    var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto);

    //    if (!validationResult.IsValid)
    //    {
    //        throw new ValidationException.ValidationException(validationResult);
    //    }
    //    var leaveAllocation = _mapper.Map<domain.LeaveAllocation>(request.LeaveAllocationDto);
    //    return Unit.Value;
    //}
}
