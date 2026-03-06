using Application.Features.Catalog.Responses;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Module.CreateModule;

public record CreateModuleCommand(
        Guid CourseId,
        string Title,
        int OrderNumber) : IRequest<Result<ModuleResponse>>;