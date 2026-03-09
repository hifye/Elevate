namespace Application.Features.Learning.Responses;

public record EnrollmentResponse(Guid StudentId, int CourseId, DateTime EnrolledAt);