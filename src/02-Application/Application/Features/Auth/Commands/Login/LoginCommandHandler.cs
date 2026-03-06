using Application.Contracts.UnitOfWork;
using Application.Features.Auth.Responses;
using Application.Interfaces.Repositories.Auth;
using Application.Interfaces.Services;
using Domain.Commom;
using Domain.ValuesObjects;
using MediatR;

namespace Application.Features.Auth.Commands.Login;

public class LoginCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, ITokenService tokenService, IPasswordHasher passwordHasher)
    : IRequestHandler<LoginCommand, Result<TokenResponse>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<Result<TokenResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = Email.Create(command.Email);
        if (user.IsFailure)
            return Result<TokenResponse>.Failure(user.Error!);

        var login = await _userRepository.GetUserByEmail(user.Value!);
        if (login == null)
            return Result<TokenResponse>.Failure("Invalid Credentials");

        var passwordValid = _passwordHasher.VerifyPassword(command.Password, login.PasswordHash);
        if (!passwordValid)
            return Result<TokenResponse>.Failure("Invalid Credentials");

        var tokenResult = _tokenService.Token(login);

        login.RefreshToken = tokenResult.RefreshToken;
        login.RefreshTokenExpiresAt = tokenResult.RefreshTokenExpiration;

        var response = new TokenResponse(
            tokenResult.Token,
            tokenResult.RefreshToken,
            tokenResult.TokenExpiration,
            tokenResult.RefreshTokenExpiration);
        
        await _unitOfWork.CommitAsync();
        return Result<TokenResponse>.Success(response);
    }
}