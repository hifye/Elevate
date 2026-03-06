using Domain.Commom;
using MediatR;

namespace Application.Features.Auth.Commands.Register;

public record RegisterCommand(
    int RoleId,
    string Name,
    string Email,
    string Password,
    string ConfirmPassword ) : IRequest<Result>;