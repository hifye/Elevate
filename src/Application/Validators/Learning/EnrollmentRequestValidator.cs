using Application.DTOs.Learning;
using FluentValidation;

namespace Application.Validators.Learning;

public class EnrollmentRequestValidator : AbstractValidator<EnrollmentRequest>
{
    public EnrollmentRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required");
        
        RuleFor(x => x.CourseId)
            .NotEmpty().WithMessage("CourseId is required");
    }   
}