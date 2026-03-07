using Domain.Commom;
using Domain.Entities.Auth;
using MediatR;

namespace Application.Features.Auth.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<Result<User>>;
