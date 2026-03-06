using Domain.Commom;
using MediatR;
using CourseResponse = Application.Features.Catalog.Responses.CourseResponse;

namespace Application.Features.Catalog.Commands.Course.UpdateCourse;

public record UpdateCourseCommand(
    Guid Id,
    string Title,
    string Description,
    decimal Price) : IRequest<Result<CourseResponse>>;