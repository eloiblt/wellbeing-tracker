using Days.Commands;
using Days.Queries;
using Infrastructure.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Days.Controllers;

[Route("user")]
public class UserController(ISender mediator) : ControllerBase
{
    [Authorize]
    [HttpGet("{*id:long}")]
    public async Task<User> Get(long id)
    {
        return await mediator.Send(new GetUserByIdQuery { Id = id });
    }
    
    [HttpPost("login")]
    public async Task<string> Login(long id)
    {
        return await mediator.Send(new LoginCommand());
    }
}