using Application.Features.Auth.Responses;
using Application.Interfaces.Repositories.Learning;
using MediatR;

namespace Application.Features.Learning.Queries.GetAllEnrollmentByStudentId;

public class GetAllEnrollmentByStudentIdQueryHandler(IEnrollmentRepository enrollmentRepository)
    : IRequestHandler<GetAllEnrollmentByStudentIdQuery, StudentResponse>
{
    private readonly IEnrollmentRepository _enrollmentRepository = enrollmentRepository;

    public Task<StudentResponse> Handle(GetAllEnrollmentByStudentIdQuery query, CancellationToken cancellationToken)
        => _enrollmentRepository.GetAllEnrollmentByStudentId(query.UserId);
}