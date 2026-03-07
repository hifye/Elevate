using Application.Features.Auth.Responses;
using Application.Features.Learning.Responses;
using Application.Interfaces.Repositories.Learning;
using Domain.Commom;
using MediatR;

namespace Application.Features.Learning.Queries.GetAllEnrollmentsByStudents;

public class GetAllEnrollmentsByStudentsQueryHandler(IEnrollmentRepository enrollmentRepository)
    : IRequestHandler<GetAllEnrollmentsByStudentsQuery, Result<IEnumerable<StudentResponse>>>
{
    private readonly IEnrollmentRepository _enrollmentRepository = enrollmentRepository;

    public async Task<Result<IEnumerable<StudentResponse>>> Handle(GetAllEnrollmentsByStudentsQuery query,
        CancellationToken cancellationToken)
    {
        var enrollments = await _enrollmentRepository.GetAllEnrollmentsByStudents();
        if (enrollments == null || !enrollments.Any())
            return Result<IEnumerable<StudentResponse>>.Failure("Students not found");
        
        return Result<IEnumerable<StudentResponse>>.Success(enrollments);
    }
}