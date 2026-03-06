using Application.Contracts.Repositories.Catalog;
using Application.Features.Catalog.Responses;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetAllWithModulesAndLessons;

public class GetAllWithModulesAndLessonsQueryHandler : IRequestHandler<GetAllWithModulesAndLessonsQuery, IEnumerable<CourseResponse>>
{
    private readonly ICourseRepository _courseRepository;

    public GetAllWithModulesAndLessonsQueryHandler(ICourseRepository courseRepository)
        => _courseRepository = courseRepository;


    public Task<IEnumerable<CourseResponse>> Handle(GetAllWithModulesAndLessonsQuery request,
        CancellationToken cancellationToken)
        => _courseRepository.GetAllWithModulesAndLessons();
}