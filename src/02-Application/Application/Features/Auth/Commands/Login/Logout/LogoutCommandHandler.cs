using Application.Abstraction.Persistance.Repositories.Auth;
using Application.Interfaces.UnitOfWork;
using Domain.Commom;
using MediatR;

namespace Application.Features.Auth.Commands.Login.Logout;

public class LogoutCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<LogoutCommand, Result>
{
    public async Task<Result> Handle(LogoutCommand query, CancellationToken cancellationToken)
    {
        var userId = await userRepository.GetUserById(query.Id);
        if (userId == null)
            return Result.Failure("User not found", "Not Found");
        
        await userRepository.Logout(query.Id);
        await unitOfWork.CommitAsync();
        return Result.Success();
    }
}