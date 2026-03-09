using Domain.ValuesObjects;

namespace Application.Features.Catalog.Responses;

public record CourseResponse(Guid CourseId, string CourseTitle, string CourseDescription, Price CoursePrice, Guid InstructorId)
{
    public List<ModuleResponse> Modules { get; init; } = [];
}