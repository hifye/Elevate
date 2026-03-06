namespace Application.Features.Catalog.Responses;

public record LessonResponse(int Id, int ModuleId, string Title, string VideoUrl, int OrderNumber);