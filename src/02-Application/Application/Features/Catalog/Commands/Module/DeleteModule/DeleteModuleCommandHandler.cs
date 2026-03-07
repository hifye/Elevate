using Application.Interfaces.Repositories.Catalog;
using Application.Interfaces.UnitOfWork;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Module.DeleteModule;

public class DeleteModuleCommandHandler(IModuleRepository moduleRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteModuleCommand, Result>
{
    public Task<Result> Handle(DeleteModuleCommand command, CancellationToken cancellationToken)
    {
        var module = moduleRepository.GetById(command.Id);
        if (module == null)
            return Task.FromResult(Result.Failure("Module not found", "Not Found"));
        
        moduleRepository.Delete(command.Id);
        unitOfWork.CommitAsync();
        return Task.FromResult(Result.Success());
    }
}