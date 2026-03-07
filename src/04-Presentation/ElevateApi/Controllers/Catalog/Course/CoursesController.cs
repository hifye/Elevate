using Application.Features.Catalog.Commands.Course.CreateCourse;
using Application.Features.Catalog.Commands.Course.DeleteCourse;
using Application.Features.Catalog.Commands.Course.UpdateCourse;
using Application.Features.Catalog.Queries.Course.GetAllCoursesByAllInstructors;
using Application.Features.Catalog.Queries.Course.GetAllWithModulesAndLessons;
using Application.Features.Catalog.Queries.Course.GetByInstructorId;
using Application.Features.Catalog.Queries.Course.GetWithModulesAndLessons;
using Application.Features.Catalog.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ElevateApi.Controllers.Catalog.Course;

[ApiController]
[Route("api/[controller]")]
public class CoursesController(Mediator mediator) : ControllerBase
{
    private readonly Mediator _mediator = mediator;
    
    [HttpGet(Name = "GetAllCoursesWithAllInstructors")]
    public async Task<ActionResult<IEnumerable<CourseResponse>>> GetAllCoursesByAllInstructors()
    {
        var result = await _mediator.Send(new GetAllCoursesByAllInstructorsQuery());
        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }

    [HttpGet("modulesAndLessons" ,Name = "GetAllWithModulesAndLessons")]
    public async Task<ActionResult<IEnumerable<CourseResponse>>> GetAllWithModulesAndLessons()
    {
        var result = await _mediator.Send(new GetAllWithModulesAndLessonsQuery());
        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }

    [HttpGet("instructor/{id:guid}", Name = "GetByInstructorId")]
    public async Task<ActionResult<CourseResponse>> GetByInstructorId(Guid id)
    {
        var result = await _mediator.Send(new GetByInstructorIdQuery(id));
        return result.IsFailure ? NotFound(result.Error) : Ok(result.Value);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CourseResponse>> GetWithModulesAndLessons(Guid id)
    {
        var result = await _mediator.Send(new GetWithModulesAndLessonsQuery(id));
        return result.IsFailure ? NotFound(result.Error) : Ok(result.Value);
    }

    [HttpPost(Name = "CreateCourse")]
    public async Task<ActionResult<CourseResponse>> CreateCourse(CreateCourseCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
    }

    [HttpPut(Name = "UpdateCourse")]
    public async Task<ActionResult> UpdateCourse(UpdateCourseCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsFailure ? NotFound(result.Error) : Ok();
    }

    [HttpDelete(Name = "DeleteCourse")]
    public async Task<ActionResult> DeleteCourse(DeleteCourseCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsFailure ? NotFound(result.Error) : NoContent();
    }
}
