using Application.Contracts.Repositories.Catalog;
using Application.Contracts.UnitOfWork;
using Application.Features.Catalog.Responses;
using AutoMapper;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Module.UpdateModule;

public class UpdateModuleCommandHandler(IModuleRepository moduleRepository, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateModuleCommand, Result<ModuleResponse>>
{
    private readonly IModuleRepository _moduleRepository = moduleRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<ModuleResponse>> Handle(UpdateModuleCommand command, CancellationToken cancellationToken)
    {
        var module = await _moduleRepository.GetById(command.Id);
        if (module == null)
            return Result<ModuleResponse>.Failure("Module cannot be null");

        var result = module.Update(command.Id, command.Title, command.OrderNumber);
        if (result.IsFailure)
            return Result<ModuleResponse>.Failure(result.Error!);

        await _moduleRepository.Update(module);
        await _unitOfWork.CommitAsync();
        return Result<ModuleResponse>.Success(_mapper.Map<ModuleResponse>(module));
    }
}