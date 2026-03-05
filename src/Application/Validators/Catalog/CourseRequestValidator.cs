using Application.DTOs.Catalog;
using FluentValidation;

namespace Application.Validators.Catalog;

public class CourseRequestValidator : AbstractValidator<CourseRequest>
{
    public CourseRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100).WithMessage("Title cannot be longer than 100 characters.");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required");
        
        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Price is required")
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.InstructorId)
            .NotEmpty().WithMessage("Instructor id is required");
    }
}