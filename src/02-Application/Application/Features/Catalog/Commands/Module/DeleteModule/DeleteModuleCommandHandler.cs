using Application.Interfaces.Repositories.Catalog;
using Application.Interfaces.UnitOfWork;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Module.DeleteModule;

public class DeleteModuleCommandHandler(IModuleRepository moduleRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteModuleCommand, Result>
{
    public async Task<Result> Handle(DeleteModuleCommand command, CancellationToken cancellationToken)
    {
        var module = await moduleRepository.GetById(command.Id);
        if (module == null)
            return Result.Failure("Module not found", "Not Found");
        
        await moduleRepository.Delete(command.Id);
        await unitOfWork.CommitAsync();
        return Result.Success();
    }
}