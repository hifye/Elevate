using Application.DTOs.Catalog;
using FluentValidation;

namespace Application.Validators.Catalog;

public class LessonRequestValidator : AbstractValidator<LessonRequest>
{
    public LessonRequestValidator()
    {
        RuleFor(x => x.ModuleId)
            .NotEmpty().WithMessage("ModuleId is required")
            .GreaterThan(0).WithMessage("ModuleId must be greater than 0");
        
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