using FluentValidation;

namespace Application.Features.Catalog.Commands.Module.CreateModule;

public class CreateModuleCommandValidator : AbstractValidator<CreateModuleCommand>
{
    public CreateModuleCommandValidator()
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