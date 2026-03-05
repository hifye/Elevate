namespace Application.DTOs.Catalog;

public record CourseRequest(
    string Title, 
    string Description, 
    decimal Price, 
    Guid InstructorId);
