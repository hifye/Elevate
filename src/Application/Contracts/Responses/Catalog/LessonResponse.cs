namespace Application.Contracts.Responses.Catalog;

public record LessonResponse(int Id, int ModuleId, string Title, string VideoUrl, int OrderNumber);
