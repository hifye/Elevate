using Application.Features.Auth.Responses;
using Domain.Commom;
using MediatR;

namespace Application.Features.Catalog.Queries.Course.GetInstructorByName;

public record GetInstructorByNameQuery(string Name) : IRequest<Result<InstructorResponse>>;