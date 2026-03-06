using Application.Features.Learning.Responses;
using Domain.ValuesObjects;

namespace Application.Features.Auth.Responses;

public record StudentResponse(Guid Id, string Name, Email Email)
{
    public List<EnrollmentResponse> Enrollments { get; init; } = [];
}