using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompetencyApp.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class addedrejectioncolstoBaseWorkFlowDatatbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateRejected",
                schema: "CoreSchema",
                table: "ReviewPeriods",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                schema: "CoreSchema",
                table: "ReviewPeriods",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RejectedBy",
                schema: "CoreSchema",
                table: "ReviewPeriods",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRejected",
                schema: "CoreSchema",
                table: "Competencies",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                schema: "CoreSchema",
                table: "Competencies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RejectedBy",
                schema: "CoreSchema",
                table: "Competencies",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateRejected",
                schema: "CoreSchema",
                table: "ReviewPeriods");

            migrationBuilder.DropColumn(
                name: "IsRejected",
                schema: "CoreSchema",
                table: "ReviewPeriods");

            migrationBuilder.DropColumn(
                name: "RejectedBy",
                schema: "CoreSchema",
                table: "ReviewPeriods");

            migrationBuilder.DropColumn(
                name: "DateRejected",
                schema: "CoreSchema",
                table: "Competencies");

            migrationBuilder.DropColumn(
                name: "IsRejected",
                schema: "CoreSchema",
                table: "Competencies");

            migrationBuilder.DropColumn(
                name: "RejectedBy",
                schema: "CoreSchema",
                table: "Competencies");
        }
    }
}
