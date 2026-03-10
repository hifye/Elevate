using Application.Features.Auth.Responses;
using Application.Features.Catalog.ListItem;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetInstructorByName;

public record GetInstructorByNameQuery(string Name) : IRequest<Result<InstructorListItem>>;