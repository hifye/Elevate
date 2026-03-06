namespace Application.Features.Learning.Responses;

public record EnrollmentResponse(Guid UserId, int CourseId, DateTime EnrolledAt);