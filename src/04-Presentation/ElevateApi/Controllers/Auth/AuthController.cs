using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Responses;
using ElevateApi.Commom.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ElevateApi.Controllers.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
   private readonly IMediator _mediator = mediator;

   [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   [ProducesResponseType(StatusCodes.Status401Unauthorized)]
   [HttpPost(Name = "Login")]
   public async Task<ActionResult<TokenResponse>> Login(LoginCommand command)
   {
      var result = await _mediator.Send(command);
      return result.ToActionResult();
   }
   
   [ProducesResponseType(StatusCodes.Status201Created)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
   [ProducesResponseType(StatusCodes.Status409Conflict)]
   [HttpPost("register", Name = "Register")]
   public async Task<ActionResult> Register(RegisterCommand command)
   {
      var result = await _mediator.Send(command);
      return result.ToActionResult();
   }
}