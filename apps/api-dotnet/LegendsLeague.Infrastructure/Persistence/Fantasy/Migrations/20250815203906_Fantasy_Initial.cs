using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LegendsLeague.Infrastructure.Persistence.Fantasy.Migrations
{
    /// <inheritdoc />
    public partial class Fantasy_Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "fantasy");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:citext", ",,");

            migrationBuilder.CreateTable(
                name: "leagues",
                schema: "fantasy",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    series_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    max_teams = table.Column<int>(type: "integer", nullable: false),
                    commissioner_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    modified_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_leagues", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "series",
                schema: "fantasy",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "citext", maxLength: 200, nullable: false),
                    season_year = table.Column<int>(type: "integer", nullable: false),
                    created_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    modified_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_series", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "drafts",
                schema: "fantasy",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    league_id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    starts_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    modified_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_drafts", x => x.id);
                    table.ForeignKey(
                        name: "f_k_drafts_leagues_league_id",
                        column: x => x.league_id,
                        principalSchema: "fantasy",
                        principalTable: "leagues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "league_members",
                schema: "fantasy",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    league_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false),
                    joined_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    modified_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_league_members", x => x.id);
                    table.ForeignKey(
                        name: "f_k_league_members_leagues_league_id",
                        column: x => x.league_id,
                        principalSchema: "fantasy",
                        principalTable: "leagues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "league_teams",
                schema: "fantasy",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    league_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    owner_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    draft_position = table.Column<int>(type: "integer", nullable: true),
                    created_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    modified_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_league_teams", x => x.id);
                    table.ForeignKey(
                        name: "f_k_league_teams_leagues_league_id",
                        column: x => x.league_id,
                        principalSchema: "fantasy",
                        principalTable: "leagues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "real_teams",
                schema: "fantasy",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    series_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    short_name = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    created_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    modified_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_real_teams", x => x.id);
                    table.ForeignKey(
                        name: "f_k_real_teams_series_series_id",
                        column: x => x.series_id,
                        principalSchema: "fantasy",
                        principalTable: "series",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "draft_picks",
                schema: "fantasy",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    draft_id = table.Column<Guid>(type: "uuid", nullable: false),
                    round_no = table.Column<int>(type: "integer", nullable: true),
                    pick_no = table.Column<int>(type: "integer", nullable: true),
                    league_team_id = table.Column<Guid>(type: "uuid", nullable: false),
                    player_id = table.Column<Guid>(type: "uuid", nullable: false),
                    made_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    modified_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_draft_picks", x => x.id);
                    table.ForeignKey(
                        name: "f_k_draft_picks_drafts_draft_id",
                        column: x => x.draft_id,
                        principalSchema: "fantasy",
                        principalTable: "drafts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_draft_picks_league_teams_league_team_id",
                        column: x => x.league_team_id,
                        principalSchema: "fantasy",
                        principalTable: "league_teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "roster_players",
                schema: "fantasy",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    league_team_id = table.Column<Guid>(type: "uuid", nullable: false),
                    player_id = table.Column<Guid>(type: "uuid", nullable: false),
                    slot = table.Column<int>(type: "integer", nullable: false),
                    active_from_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    active_to_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    modified_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_roster_players", x => x.id);
                    table.ForeignKey(
                        name: "f_k_roster_players_league_teams_league_team_id",
                        column: x => x.league_team_id,
                        principalSchema: "fantasy",
                        principalTable: "league_teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fixtures",
                schema: "fantasy",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    series_id = table.Column<Guid>(type: "uuid", nullable: false),
                    home_team_id = table.Column<Guid>(type: "uuid", nullable: false),
                    away_team_id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_time_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    modified_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_fixtures", x => x.id);
                    table.ForeignKey(
                        name: "f_k_fixtures_real_teams_away_team_id",
                        column: x => x.away_team_id,
                        principalSchema: "fantasy",
                        principalTable: "real_teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_fixtures_real_teams_home_team_id",
                        column: x => x.home_team_id,
                        principalSchema: "fantasy",
                        principalTable: "real_teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_fixtures_series_series_id",
                        column: x => x.series_id,
                        principalSchema: "fantasy",
                        principalTable: "series",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "players",
                schema: "fantasy",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    series_id = table.Column<Guid>(type: "uuid", nullable: false),
                    real_team_id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    short_name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    country = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    role = table.Column<int>(type: "integer", nullable: false),
                    batting = table.Column<int>(type: "integer", nullable: false),
                    bowling = table.Column<int>(type: "integer", nullable: false),
                    created_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    modified_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    modified_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_players", x => x.id);
                    table.ForeignKey(
                        name: "f_k_players_real_teams_real_team_id",
                        column: x => x.real_team_id,
                        principalSchema: "fantasy",
                        principalTable: "real_teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_players_series_series_id",
                        column: x => x.series_id,
                        principalSchema: "fantasy",
                        principalTable: "series",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "fantasy",
                table: "series",
                columns: new[] { "id", "created_at_utc", "created_by", "modified_at_utc", "modified_by", "name", "season_year" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, "Indian Premier League", 2026 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, "ICC T20 World Cup", 2026 }
                });

            migrationBuilder.CreateIndex(
                name: "i_x_draft_picks_draft_id_round_no_pick_no",
                schema: "fantasy",
                table: "draft_picks",
                columns: new[] { "draft_id", "round_no", "pick_no" });

            migrationBuilder.CreateIndex(
                name: "i_x_draft_picks_league_team_id_draft_id",
                schema: "fantasy",
                table: "draft_picks",
                columns: new[] { "league_team_id", "draft_id" });

            migrationBuilder.CreateIndex(
                name: "i_x_drafts_league_id_status",
                schema: "fantasy",
                table: "drafts",
                columns: new[] { "league_id", "status" });

            migrationBuilder.CreateIndex(
                name: "i_x_fixtures_away_team_id",
                schema: "fantasy",
                table: "fixtures",
                column: "away_team_id");

            migrationBuilder.CreateIndex(
                name: "i_x_fixtures_home_team_id",
                schema: "fantasy",
                table: "fixtures",
                column: "home_team_id");

            migrationBuilder.CreateIndex(
                name: "i_x_fixtures_series_id",
                schema: "fantasy",
                table: "fixtures",
                column: "series_id");

            migrationBuilder.CreateIndex(
                name: "i_x_league_members_league_id_user_id",
                schema: "fantasy",
                table: "league_members",
                columns: new[] { "league_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_league_teams_league_id_name",
                schema: "fantasy",
                table: "league_teams",
                columns: new[] { "league_id", "name" });

            migrationBuilder.CreateIndex(
                name: "i_x_league_teams_league_id_owner_user_id",
                schema: "fantasy",
                table: "league_teams",
                columns: new[] { "league_id", "owner_user_id" });

            migrationBuilder.CreateIndex(
                name: "i_x_leagues_series_id_name",
                schema: "fantasy",
                table: "leagues",
                columns: new[] { "series_id", "name" });

            migrationBuilder.CreateIndex(
                name: "i_x_players_real_team_id",
                schema: "fantasy",
                table: "players",
                column: "real_team_id");

            migrationBuilder.CreateIndex(
                name: "ix_players_full_name",
                schema: "fantasy",
                table: "players",
                column: "full_name");

            migrationBuilder.CreateIndex(
                name: "ix_players_seriesid_teamid",
                schema: "fantasy",
                table: "players",
                columns: new[] { "series_id", "real_team_id" });

            migrationBuilder.CreateIndex(
                name: "i_x_real_teams_series_id",
                schema: "fantasy",
                table: "real_teams",
                column: "series_id");

            migrationBuilder.CreateIndex(
                name: "i_x_roster_players_league_team_id_player_id_slot",
                schema: "fantasy",
                table: "roster_players",
                columns: new[] { "league_team_id", "player_id", "slot" });

            migrationBuilder.CreateIndex(
                name: "ux_series_season_year_name",
                schema: "fantasy",
                table: "series",
                columns: new[] { "season_year", "name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "draft_picks",
                schema: "fantasy");

            migrationBuilder.DropTable(
                name: "fixtures",
                schema: "fantasy");

            migrationBuilder.DropTable(
                name: "league_members",
                schema: "fantasy");

            migrationBuilder.DropTable(
                name: "players",
                schema: "fantasy");

            migrationBuilder.DropTable(
                name: "roster_players",
                schema: "fantasy");

            migrationBuilder.DropTable(
                name: "drafts",
                schema: "fantasy");

            migrationBuilder.DropTable(
                name: "real_teams",
                schema: "fantasy");

            migrationBuilder.DropTable(
                name: "league_teams",
                schema: "fantasy");

            migrationBuilder.DropTable(
                name: "series",
                schema: "fantasy");

            migrationBuilder.DropTable(
                name: "leagues",
                schema: "fantasy");
        }
    }
}
