namespace Application.DTOs.Catalog;

public record LessonRequest(
    int ModuleId, 
    string Title, 
    string VideoUrl, 
    int OrderNumber);
