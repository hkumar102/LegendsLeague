using LegendsLeague.Application;
using LegendsLeague.Infrastructure.Persistence;
using LegendsLeague.Infrastructure.Persistence.Fixtures;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog basic console
builder.Host.UseSerilog((ctx, cfg) =>
{
    cfg.ReadFrom.Configuration(ctx.Configuration)
       .Enrich.FromLogContext()
       .WriteTo.Console();
});

// Register FixturesDbContext
builder.Services.AddDbContext<FixturesDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
    npgsqlOptions =>
    {
        npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "fixtures");
    }));
    
// Register Infrastructure persistence
builder.Services.AddPersistence(builder.Configuration);

// Services
builder.Services.AddApplicationServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Legends League API",
        Version = "v1",
        Description = "Core endpoints for series/leagues (M1)"
    });
});

// CORS (dev)
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("dev", p => p
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .SetIsOriginAllowed(_ => true));
});

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseCors("dev");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Legends League API v1");
    c.RoutePrefix = "swagger";
});

app.MapControllers();
app.MapGet("/healthz", () => Results.Ok(new { status = "ok" }))
   .WithName("Health")
   .WithTags("System");

app.Run();
