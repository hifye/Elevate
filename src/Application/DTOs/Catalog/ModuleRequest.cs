namespace Application.DTOs.Catalog;

public record ModuleRequest(
    Guid CourseId, 
    string Title, 
    int OrderNumber);