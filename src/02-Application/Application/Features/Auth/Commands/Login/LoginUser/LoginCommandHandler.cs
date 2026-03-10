using Application.Abstraction.Persistance.Repositories.Auth;
using Application.Features.Auth.Responses;
using Application.Interfaces.Services;
using Application.Interfaces.UnitOfWork;
using Domain.Commom;
using Domain.ValuesObjects;
using MediatR;

namespace Application.Features.Auth.Commands.Login.LoginUser;

public class LoginCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, ITokenService tokenService, IPasswordHasher passwordHasher)
    : IRequestHandler<LoginCommand, Result<TokenResponse>>
{
    public async Task<Result<TokenResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = Email.Create(command.Email);
        if (user.IsFailure)
            return Result<TokenResponse>.Failure(user.Error!);

        var login = await userRepository.GetUserByEmail(user.Value!);
        if (login == null)
            return Result<TokenResponse>.Failure("Invalid Credentials", "Unauthorized");
        
        var passwordValid = passwordHasher.VerifyPassword(command.Password, login.PasswordHash);
        if (!passwordValid)
            return Result<TokenResponse>.Failure("Invalid Credentials", "Unauthorized");

        if (passwordHasher.NeedsRehash(login.PasswordHash))
        {
            var newHash = passwordHasher.HashPassword(command.Password);
            
            login.PasswordHash = newHash;
            await userRepository.UpdatePassword(login.Id, newHash);
        }
        
        var tokenResult = tokenService.Token(login);

        login.RefreshToken = tokenResult.RefreshToken;
        login.RefreshTokenExpiresAt = tokenResult.RefreshTokenExpiration;

        var response = new TokenResponse(
            tokenResult.Token,
            tokenResult.RefreshToken,
            tokenResult.TokenExpiration,
            tokenResult.RefreshTokenExpiration);

        await userRepository.UpdateRefreshToken(login);
        await unitOfWork.CommitAsync();
        return Result<TokenResponse>.Success(response);
    }
}