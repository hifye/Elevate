using Application.Contracts.Repositories.Catalog;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetById;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Domain.Entities.Catalog.Course >
{
    private readonly ICourseRepository _courseRepository;

    public GetByIdQueryHandler(ICourseRepository courseRepository)
        => _courseRepository = courseRepository;


    public Task<Domain.Entities.Catalog.Course> Handle(GetByIdQuery query, CancellationToken cancellationToken)
        => _courseRepository.GetById(query.Id);
}