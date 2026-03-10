using Application.Features.Auth.Responses;
using Application.Features.Learning.Commands.CreateEnrollment;
using Application.Features.Learning.Commands.DeleteEnrollment;
using Application.Features.Learning.Queries.GetAllEnrollmentsByStudents;
using ElevateApi.Commom.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ElevateApi.Controllers.Learning;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentsController(IMediator mediator) : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StudentResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("GetAllEnrollmentsByStudents", Name = "GetAllEnrollmentsByStudents")]
    public async Task<ActionResult<IEnumerable<StudentResponse>>> GetAllEnrollmentsByStudents()
    {
        var result = await mediator.Send(new GetAllEnrollmentsByStudentsQuery());
        return result.ToActionResult();
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost(Name = "CreateEnrollment")]
    public async Task<ActionResult> CreateEnrollment(CreateEnrollmentCommand command)
    {
        var result = await mediator.Send(command);
        return result.ToActionResult();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete(Name = "DeleteEnrollment")]
    public async Task<ActionResult> DeleteEnrollment(DeleteEnrollmentCommand command)
    {
        var result = await mediator.Send(command);
        return result.ToActionResult();
    }
}