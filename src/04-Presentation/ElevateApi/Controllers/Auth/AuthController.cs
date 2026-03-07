using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ElevateApi.Controllers.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
   private readonly IMediator _mediator = mediator;

   [HttpPost(Name = "Login")]
   public async Task<ActionResult<TokenResponse>> Login(LoginCommand command)
   {
      var result = await _mediator.Send(command);
      return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
   }

   [HttpPost("register", Name = "Register")]
   public async Task<ActionResult> Register(RegisterCommand command)
   {
      var result = await _mediator.Send(command);
      if(result.IsFailure) return BadRequest(result.Error);
      return Ok();
   }
}