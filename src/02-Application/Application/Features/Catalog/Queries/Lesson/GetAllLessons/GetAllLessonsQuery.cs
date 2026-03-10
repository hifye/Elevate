using Application.Features.Catalog.ListItem;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Lesson.GetAllLessons;

public record GetAllLessonsQuery : IRequest<Result<IEnumerable<LessonListItem>>>;