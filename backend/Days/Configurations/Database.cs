﻿using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Days.Configurations;

public static partial class Configuration
{
    public static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        var connBuilder = new NpgsqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));

        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DB_PASSWORD")))
        {
            connBuilder.Password = Environment.GetEnvironmentVariable("DB_PASSWORD");
        }

        if (string.IsNullOrEmpty(connBuilder.Password))
        {
            throw new Exception("Could not find a database password");
        }

        builder.Services.AddDbContext<DaysContext>(options =>
        {
            options.UseNpgsql(connBuilder.ConnectionString);

            if (builder.Environment.EnvironmentName is not "Development") return;
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });
    }
}