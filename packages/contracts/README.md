# Contracts (OpenAPI)

- CI builds the API and exports **openapi.json** as an artifact.
- Local export (no server needed):

```bash
# from repo root
dotnet build apps/api-dotnet/LegendsLeague.Api/LegendsLeague.Api.csproj -c Release
dotnet tool install -g Swashbuckle.AspNetCore.Cli || true
swagger tofile --output packages/contracts/openapi.json \
  apps/api-dotnet/LegendsLeague.Api/bin/Release/net8.0/LegendsLeague.Api.dll v1
