using Domain.Commom;
using MediatR;

namespace Application.Features.Auth.Commands.Login.Logout;

public record LogoutCommand(Guid Id) : IRequest<Result>;