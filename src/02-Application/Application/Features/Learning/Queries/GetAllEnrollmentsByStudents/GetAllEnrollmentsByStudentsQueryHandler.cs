using Application.Features.Auth.Responses;
using Application.Features.Learning.Responses;
using Application.Interfaces.Repositories.Learning;
using Domain.Commom;
using MediatR;

namespace Application.Features.Learning.Queries.GetAllEnrollmentsByStudents;

public class GetAllEnrollmentsByStudentsQueryHandler(IEnrollmentRepository enrollmentRepository)
    : IRequestHandler<GetAllEnrollmentsByStudentsQuery, Result<IEnumerable<StudentResponse>>>
{
    public async Task<Result<IEnumerable<StudentResponse>>> Handle(GetAllEnrollmentsByStudentsQuery query,
        CancellationToken cancellationToken)
    {
        var enrollments = await enrollmentRepository.GetAllEnrollmentsByStudents();
        if (enrollments == null || !enrollments.Any())
            return Result<IEnumerable<StudentResponse>>.Failure("Students not found", "Not Found");
        
        return Result<IEnumerable<StudentResponse>>.Success(enrollments);
    }
}