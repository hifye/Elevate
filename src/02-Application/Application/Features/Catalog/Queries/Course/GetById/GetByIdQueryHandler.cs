using Application.Interfaces.Repositories.Catalog;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetById;

public class GetByIdQueryHandler(ICourseRepository courseRepository)
    : IRequestHandler<GetByIdQuery, Domain.Entities.Catalog.Course>
{
    public Task<Domain.Entities.Catalog.Course> Handle(GetByIdQuery query, CancellationToken cancellationToken)
        => courseRepository.GetById(query.Id);
}