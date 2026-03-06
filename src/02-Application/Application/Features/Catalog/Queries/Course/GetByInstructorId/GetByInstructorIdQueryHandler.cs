using Application.Features.Catalog.Responses;
using Application.Interfaces.Repositories.Catalog;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetByInstructorId;

public class GetByInstructorIdQueryHandler(ICourseRepository courseRepository)
    : IRequestHandler<GetByInstructorIdQuery, CourseResponse>
{
    public Task<CourseResponse> Handle(GetByInstructorIdQuery query, CancellationToken cancellationToken)
        => courseRepository.GetByInstructorId(query.InstructorId);
}