using System.Text.Json.Serialization;
using Days.Behaviors;
using Days.Configurations;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.ConfigureDatabase();
builder.ConfigureServices();
builder.ConfigureMediator();
builder.ConfigureCors();
builder.ConfigureAuthentification();

builder.Services
    .AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Days V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors();

app.MapControllers();
app.UseHttpsRedirection();
app.UseMiddleware<ValidationExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    var appContext = scopedProvider.GetRequiredService<DaysContext>();
    try
    {
        app.Logger.LogInformation("Migrating Database...");
        await DaysContextSeed.SeedAsync(appContext, app.Logger);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.MapGet("/ping", () => "pong");

app.Run();