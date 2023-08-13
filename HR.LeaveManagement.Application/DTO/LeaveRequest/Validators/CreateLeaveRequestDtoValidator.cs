using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.DTO.LeaveRequest.Validators
{
    public class CreateLeaveRequestDtoValidator :AbstractValidator<ICreateLeaveRequestDto>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        public CreateLeaveRequestDtoValidator(ILeaveRequestRepository leaveRequestRepository)
        {            
            _leaveRequestRepository = leaveRequestRepository;


            Include(new ILeaveRequestDtoValidator(_leaveRequestRepository));
            //_leaveRequestRepository = leaveRequestRepository;

            //RuleFor(p => p.StartDate)
            //    .LessThan(p => p.EndDate).WithMessage("{PropertyName} must be before {ComparisonValue}");

            //RuleFor(p => p.EndDate)
            //  .GreaterThan(p => p.StartDate).WithMessage("{PropertyName} must be after {ComparisonValue}");


            //RuleFor(p => p.LeaveTypeId)
            //    .GreaterThan(0)
            //    .MustAsync ( async (id, token) =>
            //    {
            //        var leavetypeExists = await _leaveRequestRepository.Exists(id);
            //        return !leavetypeExists;
            //     }
            //    ).WithMessage("{PropertyName} does not exist");
        }
    }
}
