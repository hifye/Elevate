using Application.Features.Catalog.Responses;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Commands.Lesson.UpdateLesson;

public record UpdateLessonCommand(
    int Id,
    string Title,
    string VideoUrl,
    int OrderNumber) : IRequest<Result>;