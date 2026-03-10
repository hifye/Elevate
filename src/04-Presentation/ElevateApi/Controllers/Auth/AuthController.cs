using System.Security.Claims;
using Application.Features.Auth.Commands.Login.LoginUser;
using Application.Features.Auth.Commands.Login.Logout;
using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Responses;
using ElevateApi.Commom.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElevateApi.Controllers.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost(Name = "Login")]
    public async Task<ActionResult<TokenResponse>> Login(LoginCommand command)
    {
        var result = await mediator.Send(command);
        return result.ToActionResult();
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [HttpPost("register", Name = "Register")]
    public async Task<ActionResult> Register(RegisterCommand command)
    {
        var result = await mediator.Send(command);
        return result.ToActionResult();       
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("Logout", Name = "Logout")]
    [Authorize]
    public async Task<ActionResult> Logout()
    {
        var userClaims = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        if(string.IsNullOrEmpty(userClaims))
            return Unauthorized("Not Authorized");
        
        if(!Guid.TryParse(userClaims, out var id))
            return BadRequest("Invalid User Id");

        
        var command = new LogoutCommand(id);
        var result = await mediator.Send(command);
        if (result.IsSuccess)
            return NoContent();

        return result.ToActionResult();
    }
}