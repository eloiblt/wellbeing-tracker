using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FluentValidation;
using Infrastructure.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using AppContext = Infrastructure.AppContext;

namespace API.Commands;

public class LoginCommand : IRequest<string>
{
    public string? Email { get; init; }
    public string? Password { get; init; }
}

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator(AppContext context)
    {
        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress();
        
        RuleFor(c => c.Password)
            .NotEmpty()
            .MinimumLength(12);
    }
}

public class LoginCommandHandler(AppContext context) : IRequestHandler<LoginCommand, string>
{
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(
            u => u.Email == request.Email && u.Password == request.Password, cancellationToken);
        return user == null ? string.Empty : GenerateToken(user);
    }

    private static string GenerateToken(User user)
    {
        var secretKey = new SymmetricSecurityKey("superSecretKey@345superSecretKey@345"u8.ToArray());
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokeOptions = new JwtSecurityToken(
            "issuer", "audience",
            new List<Claim>
            {
                new(ClaimTypes.GivenName, user.FirstName!),
                new(ClaimTypes.Email, user.Email!),
                new(ClaimTypes.Name, user.LastName!),
                new(ClaimTypes.NameIdentifier, user.Id.ToString())
            },
            expires: DateTime.Now.AddDays(2),
            signingCredentials: signinCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
    }
}