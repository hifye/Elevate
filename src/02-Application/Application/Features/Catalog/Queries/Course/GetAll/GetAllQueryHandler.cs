using Application.Features.Catalog.Responses;
using Application.Interfaces.Repositories.Catalog;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetAll;

public class GetAllQueryHandler(ICourseRepository courseRepository)
    : IRequestHandler<GetAllQuery, Result<IEnumerable<CourseResponse>>>
{
    public async Task<Result<IEnumerable<CourseResponse>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var courses = await courseRepository.GetAll();
        if(courses == null || !courses.Any())
            return Result<IEnumerable<CourseResponse>>.Failure("Courses not found", "Not Found");

        return Result<IEnumerable<CourseResponse>>.Success(courses);
    }
}