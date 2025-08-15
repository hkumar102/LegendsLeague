using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegendsLeague.Infrastructure.Persistence.Fixtures.Migrations
{
    /// <inheritdoc />
    public partial class Fixtures_Series_CaseInsensitiveUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:citext", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "fixtures",
                table: "series",
                type: "citext",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateIndex(
                name: "ux_series_season_year_name",
                schema: "fixtures",
                table: "series",
                columns: new[] { "season_year", "name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ux_series_season_year_name",
                schema: "fixtures",
                table: "series");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:citext", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "fixtures",
                table: "series",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "citext",
                oldMaxLength: 200);
        }
    }
}
