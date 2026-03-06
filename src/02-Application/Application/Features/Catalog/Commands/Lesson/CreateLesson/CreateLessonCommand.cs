using Application.Features.Catalog.Responses;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Lesson.CreateLesson;

public record CreateLessonCommand(
    int ModuleId,
    string Title,
    string VideoUrl,
    int OrderNumber) : IRequest<Result<LessonResponse>>;