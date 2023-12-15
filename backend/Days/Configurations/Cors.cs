namespace Days.Configurations;

public static partial class Configuration
{
    public static void ConfigureCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy  =>
                {
                    policy.WithOrigins(builder.Configuration["CorsOrigin"]!);
                });
        });
    }
}