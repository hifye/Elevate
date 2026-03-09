using Application.Features.Catalog.Commands.Lesson.CreateLesson;
using Application.Features.Catalog.Commands.Lesson.DeleteLesson;
using Application.Features.Catalog.Commands.Lesson.UpdateLesson;
using Application.Features.Catalog.Queries.Lesson.GetAllLessons;
using Application.Features.Catalog.Responses;
using ElevateApi.Commom.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ElevateApi.Controllers.Catalog.Lesson;

[ApiController]
[Route("api/[controller]")]
public class LessonsController(IMediator mediator) : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LessonResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet(Name = "GetAllLessons")]
    public async Task<ActionResult<IEnumerable<LessonResponse>>> GetAllLessons()
    {
        var result = await mediator.Send(new GetAllLessonsQuery());
        return result.ToActionResult();
    }
    
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost(Name = "CreateLesson")]
    public async Task<ActionResult> CreateLesson(CreateLessonCommand command)
    {
        var result = await mediator.Send(command);
        return result.ToActionResult();
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut(Name = "UpdateLesson")]
    public async Task<ActionResult> UpdateLesson(UpdateLessonCommand command)
    {
        var result = await mediator.Send(command);
        return result.ToActionResult();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete(Name = "DeleteLesson")]
    public async Task<ActionResult> DeleteLesson(DeleteLessonCommand command)
    {
        var result = await mediator.Send(command);
        return result.ToActionResult();
    }
}