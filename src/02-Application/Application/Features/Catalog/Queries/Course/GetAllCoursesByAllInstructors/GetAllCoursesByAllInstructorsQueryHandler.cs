using Application.Features.Catalog.Responses;
using Application.Interfaces.Repositories.Catalog;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetAllCoursesByAllInstructors;

public class GetAllCoursesByAllInstructorsQueryHandler(ICourseRepository courseRepository)
    : IRequestHandler<GetAllCoursesByAllInstructorsQuery, IEnumerable<CourseResponse>>
{
    public async Task<IEnumerable<CourseResponse>> Handle(GetAllCoursesByAllInstructorsQuery request,
        CancellationToken cancellationToken)
        => await courseRepository.GetAllCoursesByAllInstructors();
}
