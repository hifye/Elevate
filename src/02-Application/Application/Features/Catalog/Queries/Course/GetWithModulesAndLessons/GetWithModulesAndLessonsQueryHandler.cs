using Application.Features.Catalog.Responses;
using Application.Interfaces.Repositories.Catalog;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetWithModulesAndLessons;

public class GetWithModulesAndLessonsQueryHandler(ICourseRepository courseRepository)
    : IRequestHandler<GetWithModulesAndLessonsQuery, IEnumerable<CourseResponse>>
{
    public Task<IEnumerable<CourseResponse>> Handle(GetWithModulesAndLessonsQuery request, CancellationToken cancellationToken)
        => courseRepository.GetAllWithModulesAndLessons();
}