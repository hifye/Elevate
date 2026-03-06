using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Course.DeleteCourse;

public record DeleteCourseCommand(Guid Id) : IRequest<Result>;