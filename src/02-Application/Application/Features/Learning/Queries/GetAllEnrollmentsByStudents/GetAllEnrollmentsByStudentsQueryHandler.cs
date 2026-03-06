using Application.Contracts.Repositories.Learning;
using Application.Features.Auth.Responses;
using Application.Features.Learning.Responses;
using MediatR;

namespace Application.Features.Learning.Queries.GetAllEnrollmentsByStudents;

public class GetAllEnrollmentsByStudentsQueryHandler(IEnrollmentRepository enrollmentRepository)
    : IRequestHandler<GetAllEnrollmentsByStudentsQuery, IEnumerable<StudentResponse>>
{
    private readonly IEnrollmentRepository _enrollmentRepository = enrollmentRepository;

    public async Task<IEnumerable<StudentResponse>> Handle(GetAllEnrollmentsByStudentsQuery query, CancellationToken cancellationToken)
        => await _enrollmentRepository.GetAllEnrollmentsByStudents();
}