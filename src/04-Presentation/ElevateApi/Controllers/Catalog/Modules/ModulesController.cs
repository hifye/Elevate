using Application.Features.Catalog.Commands.Course.DeleteCourse;
using Application.Features.Catalog.Commands.Course.UpdateCourse;
using Application.Features.Catalog.Commands.Module.CreateModule;
using Application.Features.Catalog.Responses;
using ElevateApi.Commom.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ElevateApi.Controllers.Catalog.Modules;

[ApiController]
[Route("api/[controller]")]
public class ModulesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost(Name = "CreateModule")]
    public async Task<ActionResult> CreateModule(CreateModuleCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPut(Name = "UpdateModule")]
    public async Task<ActionResult> UpdateModule(UpdateCourseCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpDelete(Name = "DeleteModule")]
    public async Task<ActionResult> DeleteModule(DeleteCourseCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsSuccess)
            NoContent();
        
        return result.ToActionResult();
    }
}