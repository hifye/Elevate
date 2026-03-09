using Application.Features.Catalog.Responses;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Lesson.GetAllLessons;

public record GetAllLessonsQuery : IRequest<Result<IEnumerable<LessonResponse>>>;