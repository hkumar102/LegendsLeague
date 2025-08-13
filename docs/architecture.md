# Architecture

## Style
- **Modular monolith** in .NET 8 (microservice-ready later).  
- Bounded contexts as modules: League, Draft, Team, Fixtures, Scoring, WaiversTrades.  
- One **DbContext per module**, one **DB schema per module**.

## Solution Layout
```
src/
  Api/                        # web host, controllers, SignalR hubs
  BuildingBlocks/             # SharedKernel, Infrastructure.Common
  Modules/
    League/{Contracts,Application,Domain,Infrastructure}
    Draft/{Contracts,Application,Domain,Infrastructure}
    Team/{...}
    Fixtures/{...}
    Scoring/{...}
    WaiversTrades/{...}
```

## Contracts
- `{Module}.Contracts/Api/V1` — request/response DTOs  
- `{Module}.Contracts/Events` — integration event payloads  
- OpenAPI published to `/packages/contracts/openapi.json`

## Realtime
- **SignalR** hubs: `/hubs/draft`, `/hubs/score`  
- Messages are **notification-only**; writes via REST/MediatR commands.

## Persistence
- PostgreSQL. Snake_case tables; C# PascalCase entities (global convention).  
- **Auditing:** created/modified/deleted + optimistic concurrency on hot rows.  
- **Soft deletes** for user-facing aggregates.

## Identity Mapping (Players)
- Canonical `players` + `player_identities(provider, provider_player_id)` for cross-provider mapping.  
- Live ingestion uses `raw_ball_events` → resolver → `ball_events` + `fantasy_points_log`.

