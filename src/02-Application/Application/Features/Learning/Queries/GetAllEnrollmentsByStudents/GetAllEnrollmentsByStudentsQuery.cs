using Application.Features.Auth.Responses;
using Application.Features.Learning.Responses;
using Domain.Commom;
using MediatR;

namespace Application.Features.Learning.Queries.GetAllEnrollmentsByStudents;

public record GetAllEnrollmentsByStudentsQuery : IRequest<Result<IEnumerable<StudentResponse>>>;
