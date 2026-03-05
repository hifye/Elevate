using Application.DTOs.Catalog;
using FluentValidation;

namespace Application.Validators.Catalog;

public class ModuleRequestValidator : AbstractValidator<ModuleRequest>
{
    public ModuleRequestValidator()
    {
        RuleFor(x => x.CourseId)
            .NotEmpty().WithMessage("CourseId is required");
        
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100).WithMessage("Title cannot be longer than 100 characters.");
        
        RuleFor(x => x.OrderNumber)
            .NotEmpty().WithMessage("Order Number is required")
            .GreaterThan(0).WithMessage("Order Number must be greater than 0");
    }
}