using Application.Features.Catalog.Responses;
using Application.Interfaces.Repositories.Catalog;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetAllWithModulesAndLessons;

public class GetAllWithModulesAndLessonsQueryHandler(ICourseRepository courseRepository)
    : IRequestHandler<GetAllWithModulesAndLessonsQuery, IEnumerable<CourseResponse>>
{
    public Task<IEnumerable<CourseResponse>> Handle(GetAllWithModulesAndLessonsQuery request,
        CancellationToken cancellationToken)
        => courseRepository.GetAllWithModulesAndLessons();
}