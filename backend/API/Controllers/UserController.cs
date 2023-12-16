using System.Security.Claims;
using API.Commands;
using API.Queries;
using Infrastructure.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("user")]
public class UserController(ISender mediator) : ControllerBase
{
    [Authorize]
    [HttpGet("current")]
    public async Task<User> GetCurrent()
    {
        var userId = long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        return await mediator.Send(new GetUserByIdQuery { Id = userId });
    }
    
    [Authorize]
    [HttpGet("{*id:long}")]
    public async Task<User> GetById(long id)
    {
        return await mediator.Send(new GetUserByIdQuery { Id = id });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
    {
        var token = await mediator.Send(loginCommand);
        return string.IsNullOrEmpty(token) ? StatusCode(401) : StatusCode(200, token);
    }

    [HttpPost("signup")]
    public Task<string> Signup([FromBody] SignupCommand signupCommand)
    {
        return mediator.Send(signupCommand);
    }
}