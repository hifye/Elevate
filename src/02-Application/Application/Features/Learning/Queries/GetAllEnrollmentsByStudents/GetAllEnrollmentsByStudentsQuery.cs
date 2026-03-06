using Application.Features.Auth.Responses;
using Application.Features.Learning.Responses;
using MediatR;

namespace Application.Features.Learning.Queries.GetAllEnrollmentsByStudents;

public record GetAllEnrollmentsByStudentsQuery : IRequest<IEnumerable<StudentResponse>>;
