namespace Application.Features.Catalog.Responses;

public record CourseResponse(Guid CourseId, string CourseTitle, string CourseDescription, decimal CoursePrice, Guid InstructorId)
{
    public List<ModuleResponse> Modules { get; init; } = [];
}