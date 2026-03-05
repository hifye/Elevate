namespace Application.Contracts.Responses.Catalog;

public record ModuleResponse(int Id, int CourseId, string Title, int OrderNumber)
{
    public List<LessonResponse> Lessons { get; init; } = [];
}