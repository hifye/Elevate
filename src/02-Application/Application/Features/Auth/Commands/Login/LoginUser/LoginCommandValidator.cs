using FluentValidation;

namespace Application.Features.Auth.Commands.Login.LoginUser;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid Email")
            .MaximumLength(200).WithMessage("Email cannot be longer than 200 characters.");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}