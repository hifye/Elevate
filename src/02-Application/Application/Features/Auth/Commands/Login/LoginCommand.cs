using Application.Features.Auth.Responses;
using Domain.Commom;
using MediatR;

namespace Application.Features.Auth.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<Result<TokenResponse>>;