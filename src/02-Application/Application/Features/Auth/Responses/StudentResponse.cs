using Application.Features.Learning.Responses;
using Domain.ValuesObjects;

namespace Application.Features.Auth.Responses;

public record StudentResponse(Guid StudentId, string StudentName, Email StudentEmail)
{
    public List<EnrollmentResponse> Enrollments { get; init; } = [];
}