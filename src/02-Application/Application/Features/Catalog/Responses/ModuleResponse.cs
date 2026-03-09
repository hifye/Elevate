namespace Application.Features.Catalog.Responses;

public record ModuleResponse(int ModuleId, Guid CourseId, string ModuleTitle, int ModuleOrderNumber)
{
    public List<LessonResponse> Lessons { get; init; } = [];
}