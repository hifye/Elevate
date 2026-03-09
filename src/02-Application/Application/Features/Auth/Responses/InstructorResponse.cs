using Domain.Entities.Catalog;
using Domain.ValuesObjects;

namespace Application.Features.Auth.Responses;

public record InstructorResponse(Guid InstructorId, string InstructorName, Email InstructorEmail)
{
    public List<Course> Courses { get; init; } = [];
}
