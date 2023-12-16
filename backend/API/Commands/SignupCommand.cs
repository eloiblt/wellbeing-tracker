using FluentValidation;
using Infrastructure.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using AppContext = Infrastructure.AppContext;

namespace API.Commands;

public class SignupCommand : IRequest<string>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public class SignupCommandValidator : AbstractValidator<SignupCommand>
{
    public SignupCommandValidator(AppContext context)
    {
    }
}

public class SignupCommandHandler(AppContext context, ISender mediator) : IRequestHandler<SignupCommand, string>
{
    public async Task<string> Handle(SignupCommand request, CancellationToken cancellationToken)
    {
        context.Users.Add(new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password
        });
        
        await context.SaveChangesAsync(cancellationToken);

        return await mediator.Send(new LoginCommand
        {
            Email = request.Email,
            Password = request.Password
        }, cancellationToken);
    }
}