# API (.NET 8)
Skeleton commands:
```bash
dotnet new webapi -n LegendsLeague.Api
# Add MediatR, FluentValidation, AutoMapper, Swashbuckle, Npgsql, Serilog, SignalR
dotnet add LegendsLeague.Api package MediatR
dotnet add LegendsLeague.Api package FluentValidation.DependencyInjectionExtensions
dotnet add LegendsLeague.Api package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add LegendsLeague.Api package Swashbuckle.AspNetCore
dotnet add LegendsLeague.Api package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add LegendsLeague.Api package Serilog.AspNetCore
dotnet add LegendsLeague.Api package Microsoft.AspNetCore.SignalR
```
Project layout (later):
```
src/
  Api/
  BuildingBlocks/
  Modules/
```
