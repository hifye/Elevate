using Application.Interfaces.Repositories.Catalog;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Module.GetById;

public class GetByIdQueryHandler(IModuleRepository _moduleRepository)
    : IRequestHandler<GetByIdQuery, Result<Domain.Entities.Catalog.Module>>
{
    public async Task<Result<Domain.Entities.Catalog.Module>> Handle(GetByIdQuery query, CancellationToken cancellationToken)
    {
        var module = await _moduleRepository.GetById(query.Id);
            if (module == null || module.Id != query.Id)
                return Result<Domain.Entities.Catalog.Module>.Failure("Module not found");
            
        return Result<Domain.Entities.Catalog.Module>.Success(module);
    }
}