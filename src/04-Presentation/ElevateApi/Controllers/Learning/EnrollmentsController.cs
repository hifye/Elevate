using Application.Features.Auth.Responses;
using Application.Features.Learning.Commands.CreateEnrollment;
using Application.Features.Learning.Commands.DeleteEnrollment;
using Application.Features.Learning.Queries.GetAllEnrollmentByStudentId;
using Application.Features.Learning.Queries.GetAllEnrollmentsByStudents;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ElevateApi.Controllers.Learning;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    
    [HttpGet("{id}", Name = "GetAllEnrollmentByStudentId")]
    public async Task<ActionResult<IEnumerable<StudentResponse>>> GetAllEnrollmentByStudentId(Guid id)
    {
        var result = await _mediator.Send(new GetAllEnrollmentByStudentIdQuery(id));
        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }

    [HttpGet(Name = "GetAllEnrollmentsByStudents")]
    public async Task<ActionResult<IEnumerable<StudentResponse>>> GetAllEnrollmentsByStudents()
    {
        var result = await _mediator.Send(new GetAllEnrollmentsByStudentsQuery());
        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }

    [HttpPost(Name = "CreateEnrollment")]
    public async Task<ActionResult> CreateEnrollment(CreateEnrollmentCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsFailure ? BadRequest(result.Error) : Ok();
    }

    [HttpDelete(Name = "DeleteEnrollment")]
    public async Task<ActionResult> DeleteEnrollment(DeleteEnrollmentCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsFailure ? NotFound(result.Error) : Ok();
    }
}