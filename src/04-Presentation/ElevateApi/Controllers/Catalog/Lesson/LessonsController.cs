using Application.Features.Catalog.Commands.Lesson.CreateLesson;
using Application.Features.Catalog.Commands.Lesson.DeleteLesson;
using Application.Features.Catalog.Commands.Lesson.UpdateLesson;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ElevateApi.Controllers.Catalog.Lesson;

[ApiController]
[Route("api/[controller]")]
public class LessonsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost(Name = "CreateLesson")]
    public async Task<ActionResult> CreateLesson(CreateLessonCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsFailure ? BadRequest(result.Error) : Ok();
    }

    [HttpPut(Name = "UpdateLesson")]
    public async Task<ActionResult> UpdateLesson(UpdateLessonCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsFailure ? NotFound(result.Error) : Ok();
    }

    [HttpDelete(Name = "DeleteLesson")]
    public async Task<ActionResult> DeleteLesson(DeleteLessonCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsFailure ? NotFound(result.Error) : NoContent();
    }
}