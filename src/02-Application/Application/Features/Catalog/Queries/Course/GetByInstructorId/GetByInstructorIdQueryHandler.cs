using Application.Contracts.Repositories.Catalog;
using Application.Features.Catalog.Responses;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetByInstructorId;

public class GetByInstructorIdQueryHandler : IRequestHandler<GetByInstructorIdQuery, CourseResponse>
{
    private readonly ICourseRepository _courseRepository;

    public GetByInstructorIdQueryHandler(ICourseRepository courseRepository)
        => _courseRepository = courseRepository;


    public Task<CourseResponse> Handle(GetByInstructorIdQuery query, CancellationToken cancellationToken)
        => _courseRepository.GetByInstructorId(query.InstructorId);
}