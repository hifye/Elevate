using Application.Features.Catalog.Responses;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Module.UpdateModule;

public record UpdateModuleCommand(
    int Id,
    string Title,
    int OrderNumber) : IRequest<Result>;