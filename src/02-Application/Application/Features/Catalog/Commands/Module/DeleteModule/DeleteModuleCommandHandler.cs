using Application.Contracts.Repositories.Catalog;
using Application.Contracts.UnitOfWork;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Module.DeleteModule;

public class DeleteModuleCommandHandler(IModuleRepository moduleRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteModuleCommand, Result>
{
    public Task<Result> Handle(DeleteModuleCommand command, CancellationToken cancellationToken)
    {
        moduleRepository.Delete(command.Id);
        unitOfWork.CommitAsync();
        return Task.FromResult(Result.Success());
    }
}