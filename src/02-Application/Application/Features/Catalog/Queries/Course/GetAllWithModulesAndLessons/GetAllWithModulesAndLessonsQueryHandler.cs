using Application.Features.Catalog.Responses;
using Application.Interfaces.Repositories.Catalog;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetAllWithModulesAndLessons;

public class GetAllWithModulesAndLessonsQueryHandler(ICourseRepository _courseRepository)
    : IRequestHandler<GetAllWithModulesAndLessonsQuery, Result<IEnumerable<CourseResponse>>>
{
    public async Task<Result<IEnumerable<CourseResponse>>> Handle(GetAllWithModulesAndLessonsQuery request,
        CancellationToken cancellationToken)
    {
        var courses = await _courseRepository.GetAllWithModulesAndLessons();
        if (courses == null || !courses.Any())
            return Result<IEnumerable<CourseResponse>>.Failure("Courses not found", "Not Found");
        
        return Result<IEnumerable<CourseResponse>>.Success(courses);
    }
}