using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Lesson.DeleteLesson;

public record DeleteLessonCommand(int Id) : IRequest<Result>;