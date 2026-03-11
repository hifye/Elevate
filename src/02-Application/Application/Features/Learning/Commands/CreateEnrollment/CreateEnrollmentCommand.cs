using Domain.Commom;
using MediatR;

namespace Application.Features.Learning.Commands.CreateEnrollment;

public record CreateEnrollmentCommand(
    Guid CourseId,
    DateTime EnrolledAt
    ) : IRequest<Result>;