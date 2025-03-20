using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompetencyApp.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class modifiedrejectedbydatatypeinBaseWorkFlowDatatbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RejectedBy",
                schema: "CoreSchema",
                table: "ReviewPeriods",
                type: "text",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "RejectedBy",
                schema: "CoreSchema",
                table: "Competencies",
                type: "text",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "RejectedBy",
                schema: "CoreSchema",
                table: "ReviewPeriods",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "RejectedBy",
                schema: "CoreSchema",
                table: "Competencies",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
