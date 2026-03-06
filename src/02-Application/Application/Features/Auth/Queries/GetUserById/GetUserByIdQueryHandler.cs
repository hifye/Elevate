using Application.Interfaces.Repositories.Auth;
using Domain.Entities.Auth;
using MediatR;

namespace Application.Features.Auth.Queries.GetUserById;

public class GetUserByIdQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, User>
{
    public Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        => userRepository.GetUserById(request.Id);
}