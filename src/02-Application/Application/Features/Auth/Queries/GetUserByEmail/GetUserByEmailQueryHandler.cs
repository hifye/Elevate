using Application.Interfaces.Repositories.Auth;
using Domain.Commom;
using Domain.Entities.Auth;
using MediatR;

namespace Application.Features.Auth.Queries.GetUserByEmail;

public class GetUserByEmailQueryHandler(IUserRepository _userRepository) : IRequestHandler<GetUserByEmailQuery, Result<User>>
{
    public async Task<Result<User>> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmail(query.Email);
            if (user == null || user.Email != query.Email)
                return Result<User>.Failure("User not found", "Not Found");

        return Result<User>.Success(user);
    }
}