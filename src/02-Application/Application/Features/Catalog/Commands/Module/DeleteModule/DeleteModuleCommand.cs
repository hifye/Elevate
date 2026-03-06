using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Module.DeleteModule;

public record DeleteModuleCommand(int Id) : IRequest<Result>;
