using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegendsLeague.Infrastructure.Persistence.Fantasy.Migrations
{
    /// <inheritdoc />
    public partial class Fantasy_Update_LeagueMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "joined_at_utc",
                schema: "fantasy",
                table: "league_members",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "invited_at_utc",
                schema: "fantasy",
                table: "league_members",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                schema: "fantasy",
                table: "league_members",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "invited_at_utc",
                schema: "fantasy",
                table: "league_members");

            migrationBuilder.DropColumn(
                name: "status",
                schema: "fantasy",
                table: "league_members");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "joined_at_utc",
                schema: "fantasy",
                table: "league_members",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }
    }
}
