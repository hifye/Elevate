using Application.Features.Catalog.Responses;
using Application.Interfaces.Repositories.Catalog;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetByInstructorId;

public class GetByInstructorIdQueryHandler(ICourseRepository _courseRepository)
    : IRequestHandler<GetByInstructorIdQuery, Result<CourseResponse>>
{
    public async Task<Result<CourseResponse>> Handle(GetByInstructorIdQuery query, CancellationToken cancellationToken)
    {
        var instructor = await _courseRepository.GetByInstructorId(query.InstructorId);
        if(instructor == null || query.InstructorId != instructor.InstructorId)
            return Result<CourseResponse>.Failure("Instructor not found");

        return Result<CourseResponse>.Success(instructor);
    }
}