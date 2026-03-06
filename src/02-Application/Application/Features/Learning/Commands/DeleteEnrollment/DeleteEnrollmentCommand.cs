using Domain.Commom;
using MediatR;

namespace Application.Features.Learning.Commands.DeleteEnrollment;

public record DeleteEnrollmentCommand(Guid UserId) : IRequest<Result>;