using FluentValidation;

namespace Application.Features.Learning.Commands.CreateEnrollment;

public class CreateEnrollmentCommandValidator : AbstractValidator<CreateEnrollmentCommand>
{
    public CreateEnrollmentCommandValidator()
    {
        RuleFor(x => x.CourseId)
            .NotEmpty().WithMessage("CourseId is required");
    }
}