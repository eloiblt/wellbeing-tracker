using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public abstract class AppContextSeed
{
    public static async Task SeedAsync(AppContext context, ILogger logger, string? env = "Development")
    {
        try
        {
            await context.Database.MigrateAsync();
            await SeedUsers(context, logger);
            await SeedMetricsType(context, logger);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine($"Environment: {env}");
            throw;
        }
    }

    private static async Task SeedUsers(AppContext context, ILogger logger)
    {
        if (await context.Users.AnyAsync()) return;

        var users = new List<User>
        {
            new() { FirstName = "Eloi", LastName = "Bellet", Email = "eloi.bellet@gmail.com" }
        };

        await context.Users.AddRangeAsync(users);
        await context.SaveChangesAsync();
        logger.LogInformation("Users seeded");
    }

    private static async Task SeedMetricsType(AppContext context, ILogger logger)
    {
        if (await context.MetricTypes.AnyAsync()) return;

        var metricTypes = new List<MetricType>
        {
            new() { Name = "Since" },
            new() { Name = "Until" },
            new() { Name = "Between" }
        };

        await context.MetricTypes.AddRangeAsync(metricTypes);
        await context.SaveChangesAsync();
        logger.LogInformation("MetricTypes seeded");
    }
}