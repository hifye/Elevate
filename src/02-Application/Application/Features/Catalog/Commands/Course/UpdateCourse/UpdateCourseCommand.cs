using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Course.UpdateCourse;

public record UpdateCourseCommand(
    Guid Id,
    string Title,
    string Description,
    decimal Price) : IRequest<Result>;