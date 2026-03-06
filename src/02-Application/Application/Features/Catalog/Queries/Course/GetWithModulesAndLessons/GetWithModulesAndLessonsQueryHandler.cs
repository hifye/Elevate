using Application.Contracts.Repositories.Catalog;
using Application.Features.Catalog.Responses;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetWithModulesAndLessons;

public class GetWithModulesAndLessonsQueryHandler : IRequestHandler<GetWithModulesAndLessonsQuery, IEnumerable<CourseResponse>>
{
    private readonly ICourseRepository _courseRepository;

    public GetWithModulesAndLessonsQueryHandler(ICourseRepository courseRepository)
        => _courseRepository = courseRepository;


    public Task<IEnumerable<CourseResponse>> Handle(GetWithModulesAndLessonsQuery request, CancellationToken cancellationToken)
        => _courseRepository.GetAllWithModulesAndLessons();
}