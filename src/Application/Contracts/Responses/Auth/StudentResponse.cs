using Application.Contracts.Responses.Learning;
using Domain.ValuesObjects;

namespace Application.Contracts.Responses.Auth;

public record StudentResponse(Guid Id, string Name, Email Email)
{
    public List<EnrollmentResponse> Enrollments { get; init; } = [];
}