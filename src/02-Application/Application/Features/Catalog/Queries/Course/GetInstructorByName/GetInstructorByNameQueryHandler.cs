using Application.Abstraction.Queries;
using Application.Features.Catalog.ListItem;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetInstructorByName;

public class GetInstructorByNameQueryHandler(ICourseQueries courseQueries)
    : IRequestHandler<GetInstructorByNameQuery, Result<InstructorListItem>>
{
    public async Task<Result<InstructorListItem>> Handle(GetInstructorByNameQuery query, CancellationToken cancellationToken)
    {
        var instructor = await courseQueries.GetInstructorByName(query.Name);
        if(instructor == null)
            return Result<InstructorListItem>.Failure("Instructor not found", "Not Found");

        return Result<InstructorListItem>.Success(instructor);
    }
}