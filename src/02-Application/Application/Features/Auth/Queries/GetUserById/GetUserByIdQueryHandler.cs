using Application.Contracts.Repositories.Auth;
using Domain.Entities.Auth;
using MediatR;

namespace Application.Features.Auth.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
        => _userRepository = userRepository;
    

    public Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        => _userRepository.GetUserById(request.Id);
}