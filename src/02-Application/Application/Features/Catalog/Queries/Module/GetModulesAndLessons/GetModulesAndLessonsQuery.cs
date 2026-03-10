using Application.Features.Catalog.ListItem;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Module.GetModulesAndLessons;

public record GetModulesAndLessonsQuery : IRequest<Result<IEnumerable<ModuleListItem>>>;