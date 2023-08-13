using FluentValidation;

namespace HR.LeaveManagement.Application.DTO.LeaveAllocation.Validators
{
    public class CreateLeaveAllocationValidator : AbstractValidator<ICreateLeaveAllocationDto>
    {
        public CreateLeaveAllocationValidator()
        {
            RuleFor(p => p.Period)
                .GreaterThan(0)
                .NotNull().WithMessage("Value cannot be null");

            RuleFor(p => p.NumberOfDays)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                .GreaterThan(0).WithMessage("{PropertyName} has to be greater than zero")
                .LessThan(100).WithMessage("{PropertyName} has to be less than 100");
        }
    }
}
