using Domain.Entities.Auth;
using Domain.ValuesObjects;
using MediatR;

namespace Application.Features.Auth.Queries.GetUserByEmail;

public record GetUserByEmailQuery(Email Email) : IRequest<User>;
