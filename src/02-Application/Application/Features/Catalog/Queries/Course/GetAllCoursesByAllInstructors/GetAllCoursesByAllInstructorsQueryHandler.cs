using Application.Features.Catalog.Responses;
using Application.Interfaces.Repositories.Catalog;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetAllCoursesByAllInstructors;

public class GetAllCoursesByAllInstructorsQueryHandler(ICourseRepository courseRepository)
    : IRequestHandler<GetAllCoursesByAllInstructorsQuery, Result<IEnumerable<CourseResponse>>>
{
    public async Task<Result<IEnumerable<CourseResponse>>> Handle(GetAllCoursesByAllInstructorsQuery query, CancellationToken cancellationToken)
    {
        var courses =  await courseRepository.GetAllCoursesByAllInstructors();
        if (courses == null || !courses.Any())
            return Result<IEnumerable<CourseResponse>>.Failure("Courses not found");
        
        return Result<IEnumerable<CourseResponse>>.Success(courses);
    }
}
