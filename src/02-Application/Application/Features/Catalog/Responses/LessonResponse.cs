namespace Application.Features.Catalog.Responses;

public record LessonResponse(int LessonId, int ModuleId, string LessonTitle, string LessonVideoUrl, int LessonOrderNumber);