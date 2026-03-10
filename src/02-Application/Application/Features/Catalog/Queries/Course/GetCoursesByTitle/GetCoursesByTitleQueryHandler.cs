using Application.Abstraction.Queries;
using Application.Features.Catalog.ListItem;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetCoursesByTitle;

public class GetCoursesByTitleQueryHandler(ICourseQueries courseQueries)
    : IRequestHandler<GetCoursesByTitleQuery, Result<IEnumerable<CourseListItem>>>
{
    public async Task<Result<IEnumerable<CourseListItem>>> Handle(GetCoursesByTitleQuery query, CancellationToken cancellationToken)
    {
        var courses = await courseQueries.GetCoursesByTitle(query.Title);
        if(courses == null || !courses.Any())
            return Result<IEnumerable<CourseListItem>>.Failure("Course not found", "Not Found");

        return Result<IEnumerable<CourseListItem>>.Success(courses);
    }
}