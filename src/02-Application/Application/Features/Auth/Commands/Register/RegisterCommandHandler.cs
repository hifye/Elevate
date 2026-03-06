using Application.Contracts.UnitOfWork;
using Application.Interfaces.Repositories.Auth;
using Application.Interfaces.Services;
using Domain.Commom;
using Domain.Entities.Auth;
using Domain.ValuesObjects;
using MediatR;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    : IRequestHandler<RegisterCommand, Result>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<Result> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var hashedPassword = _passwordHasher.HashPassword(command.Password);
        
        var emailResult = Email.Create(command.Email);
        if (emailResult.IsFailure)
            return Result.Failure(emailResult.Error!);
        
        var emailExists = await _userRepository.GetUserByEmail(emailResult.Value!);
        if(emailExists != null)
            return Result.Failure("Email already exists");
        
        var register = User.Create(command.RoleId, command.Name, command.Email, hashedPassword);
        if (register.IsFailure)
            return Result.Failure(register.Error!);
        
        await _userRepository.CreateUser(register.Value!);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }
}