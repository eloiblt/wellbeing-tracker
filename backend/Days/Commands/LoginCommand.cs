using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FluentValidation;
using Infrastructure;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Days.Commands;

public class LoginCommand : IRequest<string>
{
    public string Email { get; init; }
    public string Password { get; init; }
}

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator(DaysContext context)
    {
    }
}

public class LoginCommandHandler(DaysContext context) : IRequestHandler<LoginCommand, string>
{
    public Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var secretKey = new SymmetricSecurityKey("superSecretKey@345superSecretKey@345"u8.ToArray());
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokeOptions = new JwtSecurityToken(
            "issuer", "audience",
            claims: new List<Claim>()
            {
                new("userId", "1"),
                new("email", "a@b")
            },
            expires: DateTime.Now.AddDays(2),
            signingCredentials: signinCredentials
        );
        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(tokeOptions));
    }
}