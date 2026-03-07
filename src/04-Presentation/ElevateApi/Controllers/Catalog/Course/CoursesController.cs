using Application.Features.Catalog.Commands.Course.CreateCourse;
using Application.Features.Catalog.Commands.Course.DeleteCourse;
using Application.Features.Catalog.Commands.Course.UpdateCourse;
using Application.Features.Catalog.Queries.Course.GetAllCoursesByAllInstructors;
using Application.Features.Catalog.Queries.Course.GetAllWithModulesAndLessons;
using Application.Features.Catalog.Queries.Course.GetByInstructorId;
using Application.Features.Catalog.Queries.Course.GetWithModulesAndLessons;
using Application.Features.Catalog.Responses;
using ElevateApi.Commom.Extensions;
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
        return result.ToActionResult();
    }

    [HttpGet("modulesAndLessons" ,Name = "GetAllWithModulesAndLessons")]
    public async Task<ActionResult<IEnumerable<CourseResponse>>> GetAllWithModulesAndLessons()
    {
        var result = await _mediator.Send(new GetAllWithModulesAndLessonsQuery());
        return result.ToActionResult();
    }

    [HttpGet("instructor/{id:guid}", Name = "GetByInstructorId")]
    public async Task<ActionResult<CourseResponse>> GetByInstructorId(Guid id)
    {
        var result = await _mediator.Send(new GetByInstructorIdQuery(id));
        return result.ToActionResult();
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CourseResponse>> GetWithModulesAndLessons(Guid id)
    {
        var result = await _mediator.Send(new GetWithModulesAndLessonsQuery(id));
        return result.ToActionResult();
    }

    [HttpPost(Name = "CreateCourse")]
    public async Task<ActionResult> CreateCourse(CreateCourseCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPut(Name = "UpdateCourse")]
    public async Task<ActionResult> UpdateCourse(UpdateCourseCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpDelete(Name = "DeleteCourse")]
    public async Task<ActionResult> DeleteCourse(DeleteCourseCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsSuccess)
            NoContent();
        
        return result.ToActionResult();
    }
}
