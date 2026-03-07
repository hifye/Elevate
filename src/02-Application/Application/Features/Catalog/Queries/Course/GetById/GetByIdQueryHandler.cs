using Application.Interfaces.Repositories.Catalog;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetById;

public class GetByIdQueryHandler(ICourseRepository _courseRepository)
    : IRequestHandler<GetByIdQuery, Result<Domain.Entities.Catalog.Course>>
{
    public async Task<Result<Domain.Entities.Catalog.Course>> Handle(GetByIdQuery query, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetById(query.Id);
        if (course == null || query.Id != course.Id)
            return Result<Domain.Entities.Catalog.Course>.Failure("Course not found");
        
        return Result<Domain.Entities.Catalog.Course>.Success(course);
    }
}