using Application.Abstraction.Persistance.Repositories.Catalog;
using Application.Interfaces.UnitOfWork;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Module.CreateModule;

public class CreateModuleCommandHandler(IModuleRepository moduleRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateModuleCommand, Result>
{
    public async Task<Result> Handle(CreateModuleCommand command, CancellationToken cancellationToken)
    {
        var result = Domain.Entities.Catalog.Module.Create(command.CourseId, command.Title, command.OrderNumber);
        if (result.IsFailure)
            return Result.Failure(result.Error!);

        await moduleRepository.Create(result.Value!);
        await unitOfWork.CommitAsync();
        return Result.Success();
    }
}