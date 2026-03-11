using Application.Features.Catalog.Commands.Lesson.CreateLesson;
using Application.Features.Catalog.Commands.Lesson.DeleteLesson;
using Application.Features.Catalog.Commands.Lesson.UpdateLesson;
using Application.Features.Catalog.ListItem;
using Application.Features.Catalog.Queries.Lesson.GetAllLessons;
using ElevateApi.Commom.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElevateApi.Controllers.Catalog.Lesson;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class LessonsController(IMediator mediator) : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LessonListItem>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = "Both")]
    [HttpGet("GetAll", Name = "GetAllLessons")]
    public async Task<ActionResult<IEnumerable<LessonListItem>>> GetAllLessons()
    {
        var result = await mediator.Send(new GetAllLessonsQuery());
        return result.ToActionResult();
    }
    
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = "Instructor")]
    [HttpPost(Name = "CreateLesson")]
    public async Task<ActionResult> CreateLesson(CreateLessonCommand command)
    {
        var result = await mediator.Send(command);
        return result.ToActionResult();
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = "Instructor")]
    [HttpPut(Name = "UpdateLesson")]
    public async Task<ActionResult> UpdateLesson(UpdateLessonCommand command)
    {
        var result = await mediator.Send(command);
        return result.ToActionResult();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = "Instructor")]
    [HttpDelete(Name = "DeleteLesson")]
    public async Task<ActionResult> DeleteLesson(DeleteLessonCommand command)
    {
        var result = await mediator.Send(command);
        return result.ToActionResult();      
    }
}