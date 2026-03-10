using Application.Features.Catalog.ListItem;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetCoursesByTitle;

public record GetCoursesByTitleQuery(string Title) : IRequest<Result<IEnumerable<CourseListItem>>>;