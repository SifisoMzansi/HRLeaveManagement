using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTO.LeaveAllocation.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Requests.Commands;
using MediatR;
using domain = HR.LeaveManagement.Domain;
using ValidationException = HR.LeaveManagement.Application.Exceptions;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Handler.Commands;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;

    public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
        ;
    }

    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationValidator();
        var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException.ValidationException(validationResult);
        }
         var leaveAllocation = _mapper.Map<domain.LeaveAllocation>(request.LeaveAllocationDto);
        return Unit.Value;
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
}
