using FluentValidation;

namespace Application.Features.Catalog.Commands.Lesson.CreateLesson;

public class CreateLessonCommandValidator : AbstractValidator<CreateLessonCommand>
{
    public CreateLessonCommandValidator()
    {
        RuleFor(x => x.ModuleId)
            .NotEmpty().WithMessage("ModuleId is required");
        
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100).WithMessage("Title cannot be longer than 100 characters.");
        
        RuleFor(x => x.VideoUrl)
            .NotEmpty().WithMessage("VideoUrl is required")
            .MaximumLength(300).WithMessage("VideoUrl cannot be longer than 300 characters.");
        
        RuleFor(x => x.OrderNumber)
            .NotEmpty().WithMessage("Order Number is required")
            .GreaterThan(0).WithMessage("Order Number must be greater than 0");
    }
}