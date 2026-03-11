using Application.Features.Learning.ListItem;
using Domain.Commom;
using MediatR;

namespace Application.Features.Learning.Queries.GetAllEnrollmentsByStudents;

public record GetAllEnrollmentsByStudentsQuery : IRequest<Result<IEnumerable<StudentListItem>>>;
