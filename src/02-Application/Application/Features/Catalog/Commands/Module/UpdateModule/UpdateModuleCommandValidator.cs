using FluentValidation;

namespace Application.Features.Catalog.Commands.Module.UpdateModule;

public class UpdateModuleCommandValidator : AbstractValidator<UpdateModuleCommand>
{
    public UpdateModuleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
        
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100).WithMessage("Title cannot be longer than 100 characters.");
        
        RuleFor(x => x.OrderNumber)
            .NotEmpty().WithMessage("Order Number is required")
            .GreaterThan(0).WithMessage("Order Number must be greater than 0");
    }
}