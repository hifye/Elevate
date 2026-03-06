using Application.Contracts.Repositories.Catalog;
using MediatR;

namespace Application.Features.Catalog.Queries.Module.GetById;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Domain.Entities.Catalog.Module>
{
    private readonly IModuleRepository _moduleRepository;

    public GetByIdQueryHandler(IModuleRepository moduleRepository)
        => _moduleRepository = moduleRepository;


    public Task<Domain.Entities.Catalog.Module> Handle(GetByIdQuery query, CancellationToken cancellationToken)
        => _moduleRepository.GetById(query.Id);
}