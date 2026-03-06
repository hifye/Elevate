namespace Application.Features.Catalog.Responses;

public record ModuleResponse(int Id, Guid CourseId, string Title, int OrderNumber)
{
    public List<LessonResponse> Lessons { get; init; } = [];
}