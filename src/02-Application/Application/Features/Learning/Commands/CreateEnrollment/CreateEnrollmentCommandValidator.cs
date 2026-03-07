using FluentValidation;

namespace Application.Features.Learning.Commands.CreateEnrollment;

public class CreateEnrollmentCommandValidator : AbstractValidator<CreateEnrollmentCommand>
{
    public CreateEnrollmentCommandValidator()
    {
        RuleFor(x => x.CourseId)
            .NotEmpty().WithMessage("CourseId is required");
        
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required");
        
        RuleFor(x => x.EnrolledAt)
            .NotEmpty().WithMessage("EnrolledAt is required");       
    }
}