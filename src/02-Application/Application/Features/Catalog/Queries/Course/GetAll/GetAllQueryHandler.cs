using Application.Abstraction.Queries;
using Application.Features.Catalog.ListItem;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetAll;

public class GetAllQueryHandler(ICourseQueries courseQueries)
    : IRequestHandler<GetAllQuery, Result<IEnumerable<CourseListItem>>>
{
    public async Task<Result<IEnumerable<CourseListItem>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var courses = await courseQueries.GetAll();
        if(courses == null || !courses.Any())
            return Result<IEnumerable<CourseListItem>>.Failure("Courses not found", "Not Found");

        return Result<IEnumerable<CourseListItem>>.Success(courses);
    }
}