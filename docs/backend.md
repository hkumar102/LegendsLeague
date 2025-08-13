# Backend (.NET 8) — Setup

## Prereqs
- .NET 8 SDK
- PostgreSQL 14+
- Node 20 (for tooling), Make (optional)

## Create the host
```bash
cd apps/api-dotnet
dotnet new webapi -n LegendsLeague.Api
```

## Add core packages
```bash
dotnet add LegendsLeague.Api package MediatR
dotnet add LegendsLeague.Api package FluentValidation.DependencyInjectionExtensions
dotnet add LegendsLeague.Api package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add LegendsLeague.Api package Microsoft.AspNetCore.SignalR
dotnet add LegendsLeague.Api package Swashbuckle.AspNetCore
dotnet add LegendsLeague.Api package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add LegendsLeague.Api package Serilog.AspNetCore
```

## Minimal Program.cs sketch
```csharp
var b = WebApplication.CreateBuilder(args);
b.Services.AddControllers();
b.Services.AddEndpointsApiExplorer();
b.Services.AddSwaggerGen();
b.Services.AddSignalR();
// TODO: Add DbContexts per module, MediatR, FluentValidation, AutoMapper
var app = b.Build();
app.UseSwagger(); app.UseSwaggerUI();
app.MapControllers();
app.MapHub<DraftHub>("/hubs/draft");
app.Run();
```

## OpenAPI Export (CI)
- During CI, publish `/swagger/v1/swagger.json` to `/packages/contracts/openapi.json`.

