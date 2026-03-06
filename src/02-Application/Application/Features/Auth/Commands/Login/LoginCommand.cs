using Application.Shared.DTOs;
using MediatR;

namespace Application.Features.Auth.Commands.Login;

public record LoginCommand(string Email, string PasswordHash) : IRequest<TokenResponse>;