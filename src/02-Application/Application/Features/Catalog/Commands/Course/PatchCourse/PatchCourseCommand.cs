using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Course.PatchCourse;

public record PatchCourseCommand(
    Guid Id,
    string? Title,
    string? Description,
    decimal? Price) : IRequest<Result>;