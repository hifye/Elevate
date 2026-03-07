using Application.Interfaces.Repositories.Auth;
using Domain.Commom;
using Domain.Entities.Auth;
using MediatR;

namespace Application.Features.Auth.Queries.GetUserById;

public class GetUserByIdQueryHandler(IUserRepository _userRepository) : IRequestHandler<GetUserByIdQuery, Result<User>>
{
    public async Task<Result<User>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(query.Id);
            if(user == null || user.Id != query.Id)
                return Result<User>.Failure("User not found");
            
        return Result<User>.Success(user);
    }
}