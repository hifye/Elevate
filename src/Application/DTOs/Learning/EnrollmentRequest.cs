namespace Application.DTOs.Learning;

public record EnrollmentRequest(Guid UserId, Guid CourseId);