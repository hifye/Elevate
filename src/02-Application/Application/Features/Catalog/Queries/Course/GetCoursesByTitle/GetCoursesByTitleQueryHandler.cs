using Application.Features.Catalog.Responses;
using Application.Interfaces.Repositories.Catalog;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetCoursesByTitle;

public class GetCoursesByTitleQueryHandler(ICourseRepository courseRepository)
    : IRequestHandler<GetCoursesByTitleQuery, Result<IEnumerable<CourseResponse>>>
{
    public async Task<Result<IEnumerable<CourseResponse>>> Handle(GetCoursesByTitleQuery query, CancellationToken cancellationToken)
    {
        var courses = await courseRepository.GetCoursesByTitle(query.Title);
        if(courses == null || !courses.Any())
            return Result<IEnumerable<CourseResponse>>.Failure("Course not found", "Not Found");

        return Result<IEnumerable<CourseResponse>>.Success(courses);
    }
}