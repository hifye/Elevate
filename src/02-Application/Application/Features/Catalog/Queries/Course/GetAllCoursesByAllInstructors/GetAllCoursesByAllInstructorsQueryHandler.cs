using Application.Contracts.Repositories.Catalog;
using Application.Features.Catalog.Responses;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetAllCoursesByAllInstructors;

public class GetAllCoursesByAllInstructorsQueryHandler : IRequestHandler<GetAllCoursesByAllInstructorsQuery, IEnumerable<CourseResponse>>
{
    private readonly ICourseRepository _courseRepository;

    public GetAllCoursesByAllInstructorsQueryHandler(ICourseRepository courseRepository)
        => _courseRepository = courseRepository;
    

    public async Task<IEnumerable<CourseResponse>> Handle(GetAllCoursesByAllInstructorsQuery request,
        CancellationToken cancellationToken)
        => await _courseRepository.GetAllCoursesByAllInstructors();
}
