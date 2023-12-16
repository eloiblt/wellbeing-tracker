using Microsoft.EntityFrameworkCore;
using Npgsql;
using AppContext = Infrastructure.AppContext;

namespace API.Configurations;

public static partial class Configuration
{
    public static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
        var connBuilder = new NpgsqlConnectionStringBuilder(connectionString);

        if (!connectionString.Contains("Password"))
        {
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DB_PASSWORD")))
                connBuilder.Password = Environment.GetEnvironmentVariable("DB_PASSWORD");

            if (string.IsNullOrEmpty(connBuilder.Password)) throw new Exception("Could not find a database password");
        }

        builder.Services.AddDbContext<AppContext>(options =>
        {
            options.UseNpgsql(connBuilder.ConnectionString);

            if (builder.Environment.EnvironmentName is not "Development") return;
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });
    }
}