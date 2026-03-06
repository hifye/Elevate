using Domain.Entities.Catalog;
using Domain.ValuesObjects;

namespace Application.Features.Auth.Responses;

public record InstructorResponse(Guid Id, string Name, Email Email)
{
    public List<Course> Courses { get; init; } = [];
}
