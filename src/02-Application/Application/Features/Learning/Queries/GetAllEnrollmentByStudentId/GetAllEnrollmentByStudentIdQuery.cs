using Application.Features.Auth.Responses;
using MediatR;

namespace Application.Features.Learning.Queries.GetAllEnrollmentByStudentId;

public record GetAllEnrollmentByStudentIdQuery(Guid UserId) : IRequest<StudentResponse>;