using Domain.Entities.Catalog;
using Domain.ValuesObjects;

namespace Application.Contracts.Responses.Auth;

public record InstructorResponse(Guid Id, string Name, Email Email)
{
    public List<Course> Courses { get; init; } = [];
}
    
