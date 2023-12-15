using Days.Queries;
using Infrastructure.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Days.Controllers;

[Route("user")]
public class UserController(ISender mediator) : ControllerBase
{
    [HttpGet("{*id:long}")]
    public async Task<User> Get(long id)
    {
        return await mediator.Send(new GetUserByIdQuery { Id = id });
    }
}