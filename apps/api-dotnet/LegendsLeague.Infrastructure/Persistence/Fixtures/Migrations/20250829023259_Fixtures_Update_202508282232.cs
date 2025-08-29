using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegendsLeague.Infrastructure.Persistence.Fixtures.Migrations
{
    /// <inheritdoc />
    public partial class Fixtures_Update_202508282232 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "leagues",
                schema: "fixtures",
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
                name: "drafts",
                schema: "fixtures",
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
                        principalSchema: "fixtures",
                        principalTable: "leagues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "league_members",
                schema: "fixtures",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    league_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    invited_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    joined_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
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
                        principalSchema: "fixtures",
                        principalTable: "leagues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "league_teams",
                schema: "fixtures",
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
                        principalSchema: "fixtures",
                        principalTable: "leagues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "draft_picks",
                schema: "fixtures",
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
                        principalSchema: "fixtures",
                        principalTable: "drafts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_draft_picks_league_teams_league_team_id",
                        column: x => x.league_team_id,
                        principalSchema: "fixtures",
                        principalTable: "league_teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "roster_players",
                schema: "fixtures",
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
                        principalSchema: "fixtures",
                        principalTable: "league_teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "i_x_draft_picks_draft_id_round_no_pick_no",
                schema: "fixtures",
                table: "draft_picks",
                columns: new[] { "draft_id", "round_no", "pick_no" });

            migrationBuilder.CreateIndex(
                name: "i_x_draft_picks_league_team_id_draft_id",
                schema: "fixtures",
                table: "draft_picks",
                columns: new[] { "league_team_id", "draft_id" });

            migrationBuilder.CreateIndex(
                name: "i_x_drafts_league_id_status",
                schema: "fixtures",
                table: "drafts",
                columns: new[] { "league_id", "status" });

            migrationBuilder.CreateIndex(
                name: "i_x_league_members_league_id_user_id",
                schema: "fixtures",
                table: "league_members",
                columns: new[] { "league_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_league_teams_league_id_name",
                schema: "fixtures",
                table: "league_teams",
                columns: new[] { "league_id", "name" });

            migrationBuilder.CreateIndex(
                name: "i_x_league_teams_league_id_owner_user_id",
                schema: "fixtures",
                table: "league_teams",
                columns: new[] { "league_id", "owner_user_id" });

            migrationBuilder.CreateIndex(
                name: "i_x_leagues_series_id_name",
                schema: "fixtures",
                table: "leagues",
                columns: new[] { "series_id", "name" });

            migrationBuilder.CreateIndex(
                name: "i_x_roster_players_league_team_id_player_id_slot",
                schema: "fixtures",
                table: "roster_players",
                columns: new[] { "league_team_id", "player_id", "slot" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "draft_picks",
                schema: "fixtures");

            migrationBuilder.DropTable(
                name: "league_members",
                schema: "fixtures");

            migrationBuilder.DropTable(
                name: "roster_players",
                schema: "fixtures");

            migrationBuilder.DropTable(
                name: "drafts",
                schema: "fixtures");

            migrationBuilder.DropTable(
                name: "league_teams",
                schema: "fixtures");

            migrationBuilder.DropTable(
                name: "leagues",
                schema: "fixtures");
        }
    }
}
