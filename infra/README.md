Love that order — it mirrors the natural dependency flow (Domain → Contracts → Infrastructure → Application → API). Here’s a project‑by‑project task menu, broken down to class/enum/file level, with what’s done vs pending.

⸻

0) Conventions (applies to all)
	•	✅ Naming: snake_case in DB via convention
	•	✅ Auditing fields + soft delete (domain + EF interceptors)
	•	✅ DI split: Application vs Infrastructure
	•	✅ API version prefix: /api/v1
	•	🟨 Tests: unit in place for some features; integration pending

⸻

1) LegendsLeague.Domain

1.1 Common (base types)
	•	✅ Common/IAuditable.cs
	•	✅ Common/ISoftDeletable.cs
	•	✅ Common/AuditableEntity.cs
	•	✅ Common/SoftDeletableEntity.cs

1.2 Fixtures (real‑world)
	•	✅ Entities/Fixtures/Series.cs
	•	✅ Entities/Fixtures/RealTeam.cs
	•	✅ Entities/Fixtures/Fixture.cs
	•	✅ Entities/Fixtures/Player.cs (NEW — to add)
	•	Props: Id, SeriesId, RealTeamId, FullName, ShortName?, Country?, Role, Batting, Bowling
	•	Navs: Series, RealTeam
	•	Inherit: AuditableEntity (no soft delete unless you want it)

⸻

2) LegendsLeague.Contracts

2.1 Series
	•	✅ Series/SeriesDto.cs
	•	✅ Series/SeriesCreateRequest.cs
	•	✅ Series/SeriesUpdateRequest.cs

2.2 Teams
	•	✅ Teams/RealTeamDto.cs
	•	✅ Teams/TeamCreateRequest.cs
	•	✅ Teams/TeamUpdateRequest.cs

2.3 Players
	•	✅ Players/PlayerDto.cs
	•	✅ Players/PlayerCreateRequest.cs
	•	✅ Players/PlayerUpdateRequest.cs
	•	✅ Players/PlayerRole.cs
	•	✅ Players/BattingStyle.cs
	•	✅ Players/BowlingStyle.cs

2.4 Common
	•	✅ Common/PaginatedResult<T>.cs

⸻

3) LegendsLeague.Infrastructure

3.1 DbContexts
	•	✅ Persistence/Fixtures/FixturesDbContext.cs

3.2 Model building
	•	✅ Persistence/ModelBuilding/ModelBuilderSoftDeleteExtensions.cs
	•	✅ Persistence/Extensions/NamingConventions.cs

3.3 EntityTypeConfigurations (Fixtures schema)
	•	✅ Fixtures/Configurations/SeriesConfiguration.cs
	•	✅ Fixtures/Configurations/RealTeamConfiguration.cs
	•	✅ Fixtures/Configurations/FixtureConfiguration.cs
	•	⬜ Fixtures/Configurations/PlayerConfiguration.cs (NEW — to add)
	•	Table: players (schema: fixtures)
	•	Keys/FKs: SeriesId → series(Id), RealTeamId → real_teams(Id)
	•	Indexes: (SeriesId, RealTeamId), full_name (for ILIKE)
	•	Required: FullName, Role
	•	Optional: ShortName, Country, Batting, Bowling

3.4 Interceptors
	•	✅ Interceptors/AuditingSaveChangesInterceptor.cs
	•	✅ Interceptors/SoftDeleteSaveChangesInterceptor.cs

3.5 DI & options
	•	✅ Persistence/DependencyInjection.cs
	•	Registers: FixturesDbContext, interceptors, IFixturesDbContext

3.6 Migrations (FixturesDbContext)
	•	✅ Fixtures/Migrations/* for Series/RealTeam/Fixture initial
	•	⬜ Add migration for players table (after 1.2 + 3.3)

⸻

4) LegendsLeague.Application

4.1 DI & Pipeline
	•	✅ DependencyInjection.cs (MediatR, FluentValidation, AutoMapper, ValidationBehavior)
	•	✅ Common/Behaviors/ValidationBehavior.cs
	•	✅ Common/Mapping/MappingProfile.cs
	•	✅ Series → SeriesDto
	•	✅ RealTeam → RealTeamDto
	•	⬜ Player → PlayerDto (after domain entity)

4.2 Abstractions
	•	✅ Abstractions/Persistence/IFixturesDbContext.cs (DbSet, DbSet, DbSet)
	•	✅ add DbSet<Player> once Player entity exists
	•	✅ Abstractions/Security/ICurrentUser.cs

4.3 Pagination helpers
	•	✅ Common/Extensions/PaginationExtensions.cs (ToPaginatedResultAsync + AutoMapper overload)

4.4 Fixtures: Series (read path)
	•	✅ Features/Fixtures/Queries/GetSeriesListQuery.cs (returns PaginatedResult<SeriesDto>)
	•	✅ Features/Fixtures/Queries/GetSeriesByIdQuery.cs
	•	✅ Features/Fixtures/Queries/Validators/GetSeriesListQueryValidator.cs
	•	✅ Features/Fixtures/Queries/Validators/GetSeriesByIdQueryValidator.cs
	•	⬜ Commands (create/update/delete)
	•	⬜ Features/Fixtures/Commands/Series/CreateSeriesCommand.cs (+ validator)
	•	⬜ Features/Fixtures/Commands/Series/UpdateSeriesCommand.cs (+ validator)
	•	⬜ Features/Fixtures/Commands/Series/DeleteSeriesCommand.cs (+ validator)

4.5 Fixtures: Teams (full path)
	•	✅ Commands
	•	✅ Teams/Commands/CreateTeam/CreateTeamCommand.cs (+ validator)
	•	✅ Teams/Commands/UpdateTeam/UpdateTeamCommand.cs (+ validator)
	•	✅ Teams/Commands/DeleteTeam/DeleteTeamCommand.cs (+ validator)
	•	✅ Queries
	•	✅ Teams/Queries/GetTeamByIdQuery.cs (+ validator)
	•	✅ Teams/Queries/GetTeamsBySeriesQuery.cs (+ validator)
	•	✅ Teams/Queries/SearchTeamsQuery.cs (+ validator)

4.6 Fixtures: Players (NEW — pending)
	•	⬜ Queries
	•	⬜ Players/Queries/GetPlayerByIdQuery.cs (+ validator)
	•	⬜ Players/Queries/GetPlayersBySeriesQuery.cs (+ validator, paginated)
	•	⬜ Players/Queries/GetPlayersByTeamQuery.cs (+ validator, paginated)
	•	⬜ Players/Queries/SearchPlayersQuery.cs (+ validator, paginated; ILIKE on name/shortName/country)
	•	⬜ Commands
	•	⬜ Players/Commands/CreatePlayer/CreatePlayerCommand.cs (+ validator)
	•	⬜ Players/Commands/UpdatePlayer/UpdatePlayerCommand.cs (+ validator)
	•	⬜ Players/Commands/DeletePlayer/DeletePlayerCommand.cs (+ validator)
	•	⬜ Mapping
	•	⬜ AutoMapper: Player → PlayerDto

⸻

5) LegendsLeague.Api

5.1 Program & DI
	•	✅ Program.cs
	•	Serilog, Swagger, CORS, AddPersistence, AddApplicationServices, CurrentUser, health, apply migrations

5.2 Security
	•	✅ Security/CurrentUserAccessor.cs (implements ICurrentUser)

5.3 Controllers
	•	✅ Controllers/Fixtures/SeriesController.cs
	•	✅ GET /api/v1/series (paged list)
	•	✅ GET /api/v1/series/{id}
	•	✅ GET /api/v1/series/{seriesId}/teams
	•	✅ POST /api/v1/series/{seriesId}/teams
	•	⬜ POST/PUT/DELETE /api/v1/series (if you add Series commands)
	•	✅ Controllers/Fixtures/TeamsController.cs
	•	✅ GET /api/v1/teams/{id}
	•	✅ PUT /api/v1/teams/{id}
	•	✅ DELETE /api/v1/teams/{id}
	•	⬜ GET /api/v1/teams (global search) (optional; currently via Application only)
	•	⬜ Controllers/Fixtures/PlayersController.cs (NEW)
	•	⬜ GET /api/v1/players/{id}
	•	⬜ PUT /api/v1/players/{id}
	•	⬜ DELETE /api/v1/players/{id}
	•	⬜ GET /api/v1/series/{seriesId}/players
	•	⬜ GET /api/v1/series/{seriesId}/teams/{teamId}/players
	•	⬜ POST /api/v1/series/{seriesId}/players

5.4 Swagger
	•	✅ AddSwaggerGen basic
	•	⬜ Grouping & summaries for Players/Series commands (optional polish)

⸻

6) LegendsLeague.Tests.Unit (separate project)

6.1 Helpers
	•	✅ Testing/Fakes/FakeFixturesDbContext.cs
	•	✅ Testing/Seeding/FixturesSeed.cs
	•	✅ Testing/Mapping/TestMapper.cs

6.2 Series tests
	•	✅ Queries
	•	✅ GetSeriesListQueryHandlerTests.cs (PaginatedResult aware)
	•	✅ GetSeriesByIdQueryHandlerTests.cs
	•	⬜ Commands tests (once Series commands exist)

6.3 Teams tests
	•	✅ Queries
	•	✅ GetTeamsBySeriesQueryHandlerTests.cs
	•	⬜ Commands
	•	⬜ CreateTeamCommandHandlerTests.cs
	•	⬜ UpdateTeamCommandHandlerTests.cs
	•	⬜ DeleteTeamCommandHandlerTests.cs

6.4 Players tests (after Players feature)
	•	⬜ Queries & Commands

⸻

Recommended next steps (in this project order)
	1.	Domain
	•	Add Player.cs (1.2)
	2.	Infrastructure
	•	Add PlayerConfiguration.cs (3.3)
	•	Add migration for players (3.6)
	3.	Application
	•	Add DbSet<Player> to IFixturesDbContext (4.2)
	•	Add AutoMapper map (4.1)
	•	Add Players queries/commands/validators/handlers (4.6)
	4.	API
	•	Add PlayersController (5.3) with series‑scoped create + list, and resource get/put/delete
	5.	Tests
	•	Add Players unit tests (6.4)
	•	Add Teams command tests (6.3)

If you tell me “start with Domain/Player”, I’ll ship that as the first chunk with a bash script.