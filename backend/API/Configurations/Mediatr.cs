using API.Behaviors;
using FluentValidation;

namespace API.Configurations;

public static partial class Configuration
{
    public static void ConfigureMediator(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
        });

        builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
    }
}