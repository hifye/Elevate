using Application.Features.Catalog.Responses;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetByInstructorId;

public record GetByInstructorIdQuery(Guid InstructorId) : IRequest<CourseResponse>;
