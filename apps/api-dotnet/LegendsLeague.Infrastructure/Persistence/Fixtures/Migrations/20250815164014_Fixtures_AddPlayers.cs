using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegendsLeague.Infrastructure.Persistence.Fixtures.Migrations
{
    /// <inheritdoc />
    public partial class Fixtures_AddPlayers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "players",
                schema: "fixtures",
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
                        principalSchema: "fixtures",
                        principalTable: "real_teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "f_k_players_series_series_id",
                        column: x => x.series_id,
                        principalSchema: "fixtures",
                        principalTable: "series",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "i_x_players_real_team_id",
                schema: "fixtures",
                table: "players",
                column: "real_team_id");

            migrationBuilder.CreateIndex(
                name: "ix_players_full_name",
                schema: "fixtures",
                table: "players",
                column: "full_name");

            migrationBuilder.CreateIndex(
                name: "ix_players_seriesid_teamid",
                schema: "fixtures",
                table: "players",
                columns: new[] { "series_id", "real_team_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "players",
                schema: "fixtures");
        }
    }
}
