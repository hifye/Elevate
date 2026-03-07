using Application.Features.Auth.Responses;
using Domain.Commom;
using MediatR;

namespace Application.Features.Learning.Queries.GetAllEnrollmentByStudentId;

public record GetAllEnrollmentByStudentIdQuery(Guid UserId) : IRequest<Result<IEnumerable<StudentResponse>>>;