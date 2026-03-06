using Application.Interfaces.Repositories.Auth;
using Domain.Entities.Auth;
using MediatR;

namespace Application.Features.Auth.Queries.GetUserByEmail;

public class GetUserByEmailQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByEmailQuery, User>
{
    public Task<User> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        => userRepository.GetUserByEmail(request.Email);
}