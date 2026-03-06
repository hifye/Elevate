using Application.Contracts.Repositories.Catalog;
using Application.Contracts.UnitOfWork;
using Application.Features.Catalog.Responses;
using AutoMapper;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Module.CreateModule;

public class CreateModuleCommandHandler(IModuleRepository moduleRepository, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateModuleCommand, Result<ModuleResponse>>
{
    /// <summary>
    /// Manipula o comando de criação de um módulo, realizando as validações
    /// e persistindo os dados no repositório correspondente.
    /// </summary>
    /// <param name="command">Comando contendo os dados necessários para criar um módulo, como Id do curso, título e número da ordem.</param>
    /// <param name="cancellationToken">Token para cancelamento de operações assíncronas.</param>
    /// <returns>Um objeto <see cref="Result{T}"/> contendo a resposta com os dados do módulo criado ou informações de erro em caso de falha.</returns>
    public async Task<Result<ModuleResponse>> Handle(CreateModuleCommand command, CancellationToken cancellationToken)
    {
        var result = Domain.Entities.Catalog.Module.Create(command.CourseId, command.Title, command.OrderNumber);
        if (result.IsFailure)
            return Result<ModuleResponse>.Failure(result.Error!);

        await moduleRepository.Create(result.Value!);
        await unitOfWork.CommitAsync();
        return Result<ModuleResponse>.Success(mapper.Map<ModuleResponse>(result.Value));
    }
}