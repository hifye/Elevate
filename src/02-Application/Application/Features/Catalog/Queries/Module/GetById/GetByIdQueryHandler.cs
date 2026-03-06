using Application.Interfaces.Repositories.Catalog;
using MediatR;

namespace Application.Features.Catalog.Queries.Module.GetById;

public class GetByIdQueryHandler(IModuleRepository moduleRepository)
    : IRequestHandler<GetByIdQuery, Domain.Entities.Catalog.Module>
{
    public Task<Domain.Entities.Catalog.Module> Handle(GetByIdQuery query, CancellationToken cancellationToken)
        => moduleRepository.GetById(query.Id);
}