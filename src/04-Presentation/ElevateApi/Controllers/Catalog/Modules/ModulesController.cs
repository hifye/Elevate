using Application.Features.Catalog.Commands.Module.CreateModule;
using Application.Features.Catalog.Commands.Module.DeleteModule;
using Application.Features.Catalog.Commands.Module.PatchModule;
using Application.Features.Catalog.Commands.Module.UpdateModule;
using Application.Features.Catalog.ListItem;
using Application.Features.Catalog.Queries.Module.GetModulesAndLessons;
using ElevateApi.Commom.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElevateApi.Controllers.Catalog.Modules;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ModulesController(IMediator mediator) : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ModuleListItem>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]   
    [Authorize(Policy = "Both")]
    [HttpGet("GetAllModulesWithLessons", Name = "GetAllModulesAndLessons")]
    public async Task<ActionResult<IEnumerable<ModuleListItem>>> GetAllModulesAndLessons()
    {
        var result = await mediator.Send(new GetModulesAndLessonsQuery());
        return result.ToActionResult();
    }
    
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = "Instructor")]
    [HttpPost(Name = "CreateModule")]
    public async Task<ActionResult> CreateModule(CreateModuleCommand command)
    {
        var result = await mediator.Send(command);
        return result.ToActionResult();
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = "Instructor")]
    [HttpPut(Name = "UpdateModule")]
    public async Task<ActionResult> UpdateModule(UpdateModuleCommand command)
    {
        var result = await mediator.Send(command);
        return result.ToActionResult();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = "Instructor")]
    [HttpPatch(Name = "PatchModule")]
    public async Task<ActionResult> PatchModule(PatchModuleCommand command)
    {
        var result = await mediator.Send(command);
        return result.ToActionResult();  
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]  
    [Authorize(Policy = "Instructor")]
    [HttpDelete(Name = "DeleteModule")]
    public async Task<ActionResult> DeleteModule(DeleteModuleCommand command)
    {
        var result = await mediator.Send(command);
        return result.ToActionResult();   
    }
}