using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Module.PatchModule;

public record PatchModuleCommand(int Id, string? Title, int? OrderNumber) : IRequest<Result>;
