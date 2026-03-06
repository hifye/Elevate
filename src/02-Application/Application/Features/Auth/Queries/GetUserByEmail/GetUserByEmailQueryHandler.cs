using Application.Contracts.Repositories.Auth;
using Domain.Entities.Auth;
using MediatR;

namespace Application.Features.Auth.Queries.GetUserByEmail;

public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, User>
{
    private readonly IUserRepository _userRepository;

    public GetUserByEmailQueryHandler(IUserRepository userRepository)
        => _userRepository = userRepository;


    public Task<User> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        => _userRepository.GetUserByEmail(request.Email);
}