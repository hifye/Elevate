using Application.Abstraction.Persistance.Repositories.Auth;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWork;
using Domain.Commom;
using Domain.Entities.Auth;
using Domain.ValuesObjects;
using MediatR;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    : IRequestHandler<RegisterCommand, Result>
{
    public async Task<Result> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var hashedPassword = passwordHasher.HashPassword(command.Password);
        
        var emailResult = Email.Create(command.Email);
        if (emailResult.IsFailure)
            return Result.Failure(emailResult.Error!);
        
        var emailExists = await userRepository.GetUserByEmail(emailResult.Value!);
        if(emailExists != null)
            return Result.Failure("Email already exists", "Conflict");
        
        var register = User.Create(command.RoleId, command.Name, command.Email, hashedPassword);
        if (register.IsFailure)
            return Result.Failure(register.Error!);
        
        await userRepository.CreateUser(register.Value!);
        await unitOfWork.CommitAsync();
        return Result.Success();
    }
}