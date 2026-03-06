using Application.Features.Catalog.Responses;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetAllWithModulesAndLessons;

public class GetAllWithModulesAndLessonsQuery : IRequest<IEnumerable<CourseResponse>>;