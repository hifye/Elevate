using Application.DTOs.Auth;
using FluentValidation;

namespace Application.Validators.Auth;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid Email")
            .MaximumLength(200).WithMessage("Email cannot be longer than 200 characters.");
        
        RuleFor(x => x.PasswordHash)
            .NotEmpty().WithMessage("Password is required");
    }
}