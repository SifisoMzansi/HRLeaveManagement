using FluentValidation;

namespace HR.LeaveManagement.Application.DTO.LeaveAllocation.Validators
{
    public class UpdateLeaveAllocationValidator : AbstractValidator<ILeaveAllocationDto>
    {
        public UpdateLeaveAllocationValidator()
        {
            RuleFor(p => p.Period)
                .GreaterThan(0).WithMessage("{PropertyName} should be greater than zero")
                .NotNull().WithMessage("{PropertyName} should be greater than zero");

            RuleFor(p => p.Created )
                .NotNull().WithMessage("{PropertyName} should be greater than zero");

            RuleFor(p => p.NumberOfDays )
                .GreaterThan(0).WithMessage("{PropertyName} should be greater than zero")
                .NotNull().WithMessage("{PropertyName} should be greater than zero");
             
        }
    }
}