namespace Application.Contracts.Responses.Catalog;

public record ModuleResponse(int Id, Guid CourseId, string Title, int OrderNumber)
{
    public List<LessonResponse> Lessons { get; init; } = [];
}