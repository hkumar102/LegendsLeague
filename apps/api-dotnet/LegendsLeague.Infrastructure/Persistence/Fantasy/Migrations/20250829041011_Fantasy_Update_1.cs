using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegendsLeague.Infrastructure.Persistence.Fantasy.Migrations
{
    /// <inheritdoc />
    public partial class Fantasy_Update_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "i_x_league_teams_league_id_name",
                schema: "fantasy",
                table: "league_teams");

            migrationBuilder.DropIndex(
                name: "i_x_league_teams_league_id_owner_user_id",
                schema: "fantasy",
                table: "league_teams");

            migrationBuilder.DropIndex(
                name: "i_x_drafts_league_id_status",
                schema: "fantasy",
                table: "drafts");

            migrationBuilder.DropIndex(
                name: "i_x_draft_picks_draft_id_round_no_pick_no",
                schema: "fantasy",
                table: "draft_picks");

            migrationBuilder.DropIndex(
                name: "i_x_draft_picks_league_team_id_draft_id",
                schema: "fantasy",
                table: "draft_picks");

            migrationBuilder.DropColumn(
                name: "draft_position",
                schema: "fantasy",
                table: "league_teams");

            migrationBuilder.DropColumn(
                name: "starts_at_utc",
                schema: "fantasy",
                table: "drafts");

            migrationBuilder.DropColumn(
                name: "made_at_utc",
                schema: "fantasy",
                table: "draft_picks");

            migrationBuilder.DropColumn(
                name: "pick_no",
                schema: "fantasy",
                table: "draft_picks");

            migrationBuilder.DropColumn(
                name: "round_no",
                schema: "fantasy",
                table: "draft_picks");

            migrationBuilder.RenameColumn(
                name: "commissioner_user_id",
                schema: "fantasy",
                table: "leagues",
                newName: "commissioner_id");

            migrationBuilder.RenameColumn(
                name: "owner_user_id",
                schema: "fantasy",
                table: "league_teams",
                newName: "owner_id");

            migrationBuilder.RenameColumn(
                name: "type",
                schema: "fantasy",
                table: "drafts",
                newName: "draft_type");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "fantasy",
                table: "league_teams",
                type: "character varying(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(80)",
                oldMaxLength: 80);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "completed_at_utc",
                schema: "fantasy",
                table: "drafts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "scheduled_at_utc",
                schema: "fantasy",
                table: "drafts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "started_at_utc",
                schema: "fantasy",
                table: "drafts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "overall_pick_number",
                schema: "fantasy",
                table: "draft_picks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "pick_in_round",
                schema: "fantasy",
                table: "draft_picks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "picked_at_utc",
                schema: "fantasy",
                table: "draft_picks",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "round_number",
                schema: "fantasy",
                table: "draft_picks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "status",
                schema: "fantasy",
                table: "draft_picks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "series",
                schema: "fantasy",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
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
                name: "real_team",
                schema: "fantasy",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    series_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    short_name = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("p_k_real_team", x => x.id);
                    table.ForeignKey(
                        name: "f_k_real_team_series_series_id",
                        column: x => x.series_id,
                        principalSchema: "fantasy",
                        principalTable: "series",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fixture",
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
                    table.PrimaryKey("p_k_fixture", x => x.id);
                    table.ForeignKey(
                        name: "f_k_fixture_real_team_away_team_id",
                        column: x => x.away_team_id,
                        principalSchema: "fantasy",
                        principalTable: "real_team",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_fixture_real_team_home_team_id",
                        column: x => x.home_team_id,
                        principalSchema: "fantasy",
                        principalTable: "real_team",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_fixture_series_series_id",
                        column: x => x.series_id,
                        principalSchema: "fantasy",
                        principalTable: "series",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "player",
                schema: "fantasy",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    series_id = table.Column<Guid>(type: "uuid", nullable: false),
                    real_team_id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "text", nullable: false),
                    short_name = table.Column<string>(type: "text", nullable: true),
                    country = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("p_k_player", x => x.id);
                    table.ForeignKey(
                        name: "f_k_player_real_team_real_team_id",
                        column: x => x.real_team_id,
                        principalSchema: "fantasy",
                        principalTable: "real_team",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_player_series_series_id",
                        column: x => x.series_id,
                        principalSchema: "fantasy",
                        principalTable: "series",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "i_x_league_teams_owner_id",
                schema: "fantasy",
                table: "league_teams",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "ux_league_teams_league_name",
                schema: "fantasy",
                table: "league_teams",
                columns: new[] { "league_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ux_league_teams_league_owner",
                schema: "fantasy",
                table: "league_teams",
                columns: new[] { "league_id", "owner_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_drafts_league_id",
                schema: "fantasy",
                table: "drafts",
                column: "league_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_draft_picks_player_id",
                schema: "fantasy",
                table: "draft_picks",
                column: "player_id");

            migrationBuilder.CreateIndex(
                name: "ix_draft_picks_league_team",
                schema: "fantasy",
                table: "draft_picks",
                column: "league_team_id");

            migrationBuilder.CreateIndex(
                name: "ux_draft_picks_draft_overall",
                schema: "fantasy",
                table: "draft_picks",
                columns: new[] { "draft_id", "overall_pick_number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ux_draft_picks_draft_player",
                schema: "fantasy",
                table: "draft_picks",
                columns: new[] { "draft_id", "player_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_fixture_away_team_id",
                schema: "fantasy",
                table: "fixture",
                column: "away_team_id");

            migrationBuilder.CreateIndex(
                name: "i_x_fixture_home_team_id",
                schema: "fantasy",
                table: "fixture",
                column: "home_team_id");

            migrationBuilder.CreateIndex(
                name: "i_x_fixture_series_id",
                schema: "fantasy",
                table: "fixture",
                column: "series_id");

            migrationBuilder.CreateIndex(
                name: "i_x_player_real_team_id",
                schema: "fantasy",
                table: "player",
                column: "real_team_id");

            migrationBuilder.CreateIndex(
                name: "i_x_player_series_id",
                schema: "fantasy",
                table: "player",
                column: "series_id");

            migrationBuilder.CreateIndex(
                name: "i_x_real_team_series_id",
                schema: "fantasy",
                table: "real_team",
                column: "series_id");

            migrationBuilder.AddForeignKey(
                name: "f_k_draft_picks_player_player_id",
                schema: "fantasy",
                table: "draft_picks",
                column: "player_id",
                principalSchema: "fantasy",
                principalTable: "player",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_league_teams_league_members_owner_id",
                schema: "fantasy",
                table: "league_teams",
                column: "owner_id",
                principalSchema: "fantasy",
                principalTable: "league_members",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "f_k_leagues_series_series_id",
                schema: "fantasy",
                table: "leagues",
                column: "series_id",
                principalSchema: "fantasy",
                principalTable: "series",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_draft_picks_player_player_id",
                schema: "fantasy",
                table: "draft_picks");

            migrationBuilder.DropForeignKey(
                name: "f_k_league_teams_league_members_owner_id",
                schema: "fantasy",
                table: "league_teams");

            migrationBuilder.DropForeignKey(
                name: "f_k_leagues_series_series_id",
                schema: "fantasy",
                table: "leagues");

            migrationBuilder.DropTable(
                name: "fixture",
                schema: "fantasy");

            migrationBuilder.DropTable(
                name: "player",
                schema: "fantasy");

            migrationBuilder.DropTable(
                name: "real_team",
                schema: "fantasy");

            migrationBuilder.DropTable(
                name: "series",
                schema: "fantasy");

            migrationBuilder.DropIndex(
                name: "i_x_league_teams_owner_id",
                schema: "fantasy",
                table: "league_teams");

            migrationBuilder.DropIndex(
                name: "ux_league_teams_league_name",
                schema: "fantasy",
                table: "league_teams");

            migrationBuilder.DropIndex(
                name: "ux_league_teams_league_owner",
                schema: "fantasy",
                table: "league_teams");

            migrationBuilder.DropIndex(
                name: "i_x_drafts_league_id",
                schema: "fantasy",
                table: "drafts");

            migrationBuilder.DropIndex(
                name: "i_x_draft_picks_player_id",
                schema: "fantasy",
                table: "draft_picks");

            migrationBuilder.DropIndex(
                name: "ix_draft_picks_league_team",
                schema: "fantasy",
                table: "draft_picks");

            migrationBuilder.DropIndex(
                name: "ux_draft_picks_draft_overall",
                schema: "fantasy",
                table: "draft_picks");

            migrationBuilder.DropIndex(
                name: "ux_draft_picks_draft_player",
                schema: "fantasy",
                table: "draft_picks");

            migrationBuilder.DropColumn(
                name: "completed_at_utc",
                schema: "fantasy",
                table: "drafts");

            migrationBuilder.DropColumn(
                name: "scheduled_at_utc",
                schema: "fantasy",
                table: "drafts");

            migrationBuilder.DropColumn(
                name: "started_at_utc",
                schema: "fantasy",
                table: "drafts");

            migrationBuilder.DropColumn(
                name: "overall_pick_number",
                schema: "fantasy",
                table: "draft_picks");

            migrationBuilder.DropColumn(
                name: "pick_in_round",
                schema: "fantasy",
                table: "draft_picks");

            migrationBuilder.DropColumn(
                name: "picked_at_utc",
                schema: "fantasy",
                table: "draft_picks");

            migrationBuilder.DropColumn(
                name: "round_number",
                schema: "fantasy",
                table: "draft_picks");

            migrationBuilder.DropColumn(
                name: "status",
                schema: "fantasy",
                table: "draft_picks");

            migrationBuilder.RenameColumn(
                name: "commissioner_id",
                schema: "fantasy",
                table: "leagues",
                newName: "commissioner_user_id");

            migrationBuilder.RenameColumn(
                name: "owner_id",
                schema: "fantasy",
                table: "league_teams",
                newName: "owner_user_id");

            migrationBuilder.RenameColumn(
                name: "draft_type",
                schema: "fantasy",
                table: "drafts",
                newName: "type");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "fantasy",
                table: "league_teams",
                type: "character varying(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(120)",
                oldMaxLength: 120);

            migrationBuilder.AddColumn<int>(
                name: "draft_position",
                schema: "fantasy",
                table: "league_teams",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "starts_at_utc",
                schema: "fantasy",
                table: "drafts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "made_at_utc",
                schema: "fantasy",
                table: "draft_picks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "pick_no",
                schema: "fantasy",
                table: "draft_picks",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "round_no",
                schema: "fantasy",
                table: "draft_picks",
                type: "integer",
                nullable: true);

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
                name: "i_x_drafts_league_id_status",
                schema: "fantasy",
                table: "drafts",
                columns: new[] { "league_id", "status" });

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
        }
    }
}
