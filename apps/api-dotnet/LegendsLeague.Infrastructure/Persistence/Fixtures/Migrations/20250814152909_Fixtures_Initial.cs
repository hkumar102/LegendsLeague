using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LegendsLeague.Infrastructure.Persistence.Fixtures.Migrations
{
    /// <inheritdoc />
    public partial class Fixtures_Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "fixtures");

            migrationBuilder.CreateTable(
                name: "series",
                schema: "fixtures",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
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
                name: "real_teams",
                schema: "fixtures",
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
                        principalSchema: "fixtures",
                        principalTable: "series",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fixtures",
                schema: "fixtures",
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
                        principalSchema: "fixtures",
                        principalTable: "real_teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_fixtures_real_teams_home_team_id",
                        column: x => x.home_team_id,
                        principalSchema: "fixtures",
                        principalTable: "real_teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_fixtures_series_series_id",
                        column: x => x.series_id,
                        principalSchema: "fixtures",
                        principalTable: "series",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "fixtures",
                table: "series",
                columns: new[] { "id", "created_at_utc", "created_by", "modified_at_utc", "modified_by", "name", "season_year" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, "Indian Premier League", 2026 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, "ICC T20 World Cup", 2026 }
                });

            migrationBuilder.CreateIndex(
                name: "i_x_fixtures_away_team_id",
                schema: "fixtures",
                table: "fixtures",
                column: "away_team_id");

            migrationBuilder.CreateIndex(
                name: "i_x_fixtures_home_team_id",
                schema: "fixtures",
                table: "fixtures",
                column: "home_team_id");

            migrationBuilder.CreateIndex(
                name: "i_x_fixtures_series_id",
                schema: "fixtures",
                table: "fixtures",
                column: "series_id");

            migrationBuilder.CreateIndex(
                name: "i_x_real_teams_series_id",
                schema: "fixtures",
                table: "real_teams",
                column: "series_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fixtures",
                schema: "fixtures");

            migrationBuilder.DropTable(
                name: "real_teams",
                schema: "fixtures");

            migrationBuilder.DropTable(
                name: "series",
                schema: "fixtures");
        }
    }
}
