using MediatR;
using CourseResponse = Application.Features.Catalog.Responses.CourseResponse;

namespace Application.Features.Catalog.Queries.Course.GetAllCoursesByAllInstructors;

public record GetAllCoursesByAllInstructorsQuery : IRequest<IEnumerable<CourseResponse>>;