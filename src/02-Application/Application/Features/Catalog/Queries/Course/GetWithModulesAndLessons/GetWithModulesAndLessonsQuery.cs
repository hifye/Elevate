using Application.Features.Catalog.Responses;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetWithModulesAndLessons;

public class GetWithModulesAndLessonsQuery : IRequest<IEnumerable<CourseResponse>>;