using Application.Features.Auth.Responses;
using Application.Interfaces.Repositories.Catalog;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetInstructorByName;

public class GetInstructorByNameQueryHandler(ICourseRepository courseRepository)
    : IRequestHandler<GetInstructorByNameQuery, Result<InstructorResponse>>
{
    public async Task<Result<InstructorResponse>> Handle(GetInstructorByNameQuery query, CancellationToken cancellationToken)
    {
        var instructor = await courseRepository.GetInstructorByName(query.Name);
        if(instructor == null)
            return Result<InstructorResponse>.Failure("Instructor not found", "Not Found");

        return Result<InstructorResponse>.Success(instructor);
    }
}