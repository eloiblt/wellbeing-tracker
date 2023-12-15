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
                    policy.AllowAnyHeader().WithOrigins(builder.Configuration["CorsOrigin"]!);
                });
        });
    }
}