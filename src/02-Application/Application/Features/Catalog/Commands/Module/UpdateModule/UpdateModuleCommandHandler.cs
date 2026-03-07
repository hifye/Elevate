using Application.Features.Catalog.Responses;
using Application.Interfaces.Repositories.Catalog;
using Application.Interfaces.UnitOfWork;
using AutoMapper;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Module.UpdateModule;

public class UpdateModuleCommandHandler(IModuleRepository moduleRepository, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateModuleCommand, Result>
{
    private readonly IModuleRepository _moduleRepository = moduleRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> Handle(UpdateModuleCommand command, CancellationToken cancellationToken)
    {
        var module = await _moduleRepository.GetById(command.Id);
        if (module == null)
            return Result.Failure("Module Not Found", "Not Found");

        var result = module.Update(command.Id, command.Title, command.OrderNumber);
        if (result.IsFailure)
            return Result.Failure(result.Error!);

        await _moduleRepository.Update(module);
        await _unitOfWork.CommitAsync();
        return Result.Success();
    }
}