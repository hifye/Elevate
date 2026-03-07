using Application.Features.Auth.Responses;
using Application.Interfaces.Repositories.Learning;
using Domain.Commom;
using MediatR;

namespace Application.Features.Learning.Queries.GetAllEnrollmentByStudentId;

public class GetAllEnrollmentByStudentIdQueryHandler(IEnrollmentRepository enrollmentRepository)
    : IRequestHandler<GetAllEnrollmentByStudentIdQuery, Result<IEnumerable<StudentResponse>>>
{
    private readonly IEnrollmentRepository _enrollmentRepository = enrollmentRepository;

    public async Task<Result<IEnumerable<StudentResponse>>> Handle(GetAllEnrollmentByStudentIdQuery query, CancellationToken cancellationToken)
    {
        var enrollments = await _enrollmentRepository.GetAllEnrollmentByStudentId(query.UserId);
        if (enrollments == null || !enrollments.Any())
            return Result<IEnumerable<StudentResponse>>.Failure("Student not found");
        
        return Result<IEnumerable<StudentResponse>>.Success(enrollments);
    }
}