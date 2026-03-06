using Application.Features.Learning.Responses;
using Domain.Commom;
using MediatR;

namespace Application.Features.Learning.Commands.Create;

public record CreateEnrollmentCommand(
    Guid UserId,
    Guid CourseId,
    DateTime EnrolledAt
    ) : IRequest<Result<EnrollmentResponse>>;