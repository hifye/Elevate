using Application.Features.Catalog.Responses;
using Application.Interfaces.Repositories.Catalog;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetWithModulesAndLessons;

public class GetWithModulesAndLessonsQueryHandler(ICourseRepository _courseRepository)
    : IRequestHandler<GetWithModulesAndLessonsQuery, Result<CourseResponse>>
{
    public async Task<Result<CourseResponse>> Handle(GetWithModulesAndLessonsQuery query, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetWithModulesAndLessons(query.Id);
        if (course == null || query.Id != course.Id)  
                return Result<CourseResponse>.Failure("Course not found", "Not Found");
        
        return Result<CourseResponse>.Success(course);
    }
}