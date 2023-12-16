using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Configurations;

public static partial class Configuration
{
    public static void ConfigureAuthentification(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = "issuer",
                    ValidateIssuerSigningKey = true,
                    ValidAudience = "audience",
                    IssuerSigningKey = new SymmetricSecurityKey("superSecretKey@345superSecretKey@345"u8.ToArray())
                };
            });
    }
}