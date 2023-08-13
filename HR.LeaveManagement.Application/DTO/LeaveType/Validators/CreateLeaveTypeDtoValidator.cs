using FluentValidation;

namespace HR.LeaveManagement.Application.DTO.LeaveType.Validators
{
    public class CreateLeaveTypeDtoValidator: AbstractValidator<ICreateLeaveTypeDto>
    {
        public CreateLeaveTypeDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull().WithMessage("{PropertyName} cannot exceed 50 characters");

            RuleFor(p => p.DefaultDays)
                    .NotNull().WithMessage("{PropertyName} is required")
                    .LessThan(100).WithMessage("{PropertyName} should be less than 100")
                    .GreaterThan(0).WithMessage("{PropertyName} should be greater than 0");
        }
         
         
    }
}
