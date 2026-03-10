using Application.Abstraction.Queries;
using Application.Features.Learning.ListItem;
using Domain.Commom;
using MediatR;

namespace Application.Features.Learning.Queries.GetAllEnrollmentsByStudents;

public class GetAllEnrollmentsByStudentsQueryHandler(IEnrollmentQueries enrollmentQueries)
    : IRequestHandler<GetAllEnrollmentsByStudentsQuery, Result<IEnumerable<StudentListItem>>>
{
    public async Task<Result<IEnumerable<StudentListItem>>> Handle(GetAllEnrollmentsByStudentsQuery query,
        CancellationToken cancellationToken)
    {
        var enrollments = await enrollmentQueries.GetAllEnrollmentsByStudents();
        if (enrollments == null || !enrollments.Any())
            return Result<IEnumerable<StudentListItem>>.Failure("Students not found", "Not Found");
        
        return Result<IEnumerable<StudentListItem>>.Success(enrollments);
    }
}