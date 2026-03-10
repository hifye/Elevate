using Application.Features.Auth.Responses;
using Application.Features.Catalog.Commands.Course.CreateCourse;
using Application.Features.Catalog.Commands.Course.DeleteCourse;
using Application.Features.Catalog.Commands.Course.UpdateCourse;
using Application.Features.Catalog.Queries.Course.GetAll;
using Application.Features.Catalog.Queries.Course.GetCoursesByTitle;
using Application.Features.Catalog.Queries.Course.GetInstructorByName;
using Application.Features.Catalog.Responses;
using ElevateApi.Commom.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ElevateApi.Controllers.Catalog.Course;

[ApiController]
[Route("api/[controller]")]
public class CoursesController(IMediator mediator) : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CourseResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("GetAll", Name = "GetAllCourses")]
    public async Task<ActionResult<IEnumerable<CourseResponse>>> GetAllCourses()
    {
        var result = await mediator.Send(new GetAllQuery());
        return result.ToActionResult();
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CourseResponse>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("GetCoursesByTitle/{title}", Name = "GetCoursesByTitle")]
    public async Task<ActionResult<IEnumerable<CourseResponse>>> GetCoursesByTitle(string title)
    {
        var result = await mediator.Send(new GetCoursesByTitleQuery(title));
        return result.ToActionResult();
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InstructorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("GetInstructor/{name}", Name = "GetInstructorByName")]
    public async Task<ActionResult<InstructorResponse>> GetInstructorByName(string name)
    {
        var result = await mediator.Send(new GetInstructorByNameQuery(name));
        return result.ToActionResult();
    }
    
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost(Name = "CreateCourse")]
    public async Task<ActionResult> CreateCourse(CreateCourseCommand command)
    {
        var result = await mediator.Send(command);
        if (result.IsSuccess)
            Created();
            
        return result.ToActionResult();
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut(Name = "UpdateCourse")]
    public async Task<ActionResult> UpdateCourse(UpdateCourseCommand command)
    {
        var result = await mediator.Send(command);
        if (result.IsSuccess)
            Ok();
        
        return result.ToActionResult();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete(Name = "DeleteCourse")]
    public async Task<ActionResult> DeleteCourse(DeleteCourseCommand command)
    {
        var result = await mediator.Send(command);
        if (result.IsSuccess)
            NoContent();
        
        return result.ToActionResult();
    }
}
