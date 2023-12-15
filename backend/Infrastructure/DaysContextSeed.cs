using System.Collections.Specialized;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public abstract class DaysContextSeed
{
    public static async Task SeedAsync(DaysContext context, ILogger logger, string? env = "Development")
    {
        try
        {
            await context.Database.MigrateAsync();
            await SeedUsers(context, logger);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine($"Environment: {env}");
            throw;
        }
    }

    private static async Task SeedUsers(DaysContext context, ILogger logger)
    {
        if (await context.Users.AnyAsync())
        {
            return;
        }

        var users = new List<User>
        {
            new() { FirstName = "Eloi", LastName = "Bellet", Email = "eloi.bellet@gmail.com" },
        };

        await context.Users.AddRangeAsync(users);
        await context.SaveChangesAsync();
        logger.LogInformation("Users seeded");
    }
}