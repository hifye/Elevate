using Application.Abstraction.Persistance.Repositories.Catalog;
using Application.Interfaces.UnitOfWork;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Module.PatchModule;

public class PatchModuleCommandHandler(IModuleRepository moduleRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<PatchModuleCommand, Result>
{
    public async Task<Result> Handle(PatchModuleCommand command, CancellationToken cancellationToken)
    {
        var patch = await moduleRepository.GetById(command.Id);
        if (patch is null)
            return Result.Failure("Module not found", "Not Found");
        
        var result = patch.ApplyPatch(command.Title, command.OrderNumber);
        if (result.IsFailure)
            return result;
        
        await moduleRepository.Update(patch);
        await unitOfWork.CommitAsync();
        return Result.Success();       
    }
}