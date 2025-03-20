using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompetencyApp.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class addedrejectionreasoncolinBaseWorkFlowDatatbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                schema: "CoreSchema",
                table: "ReviewPeriods",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                schema: "CoreSchema",
                table: "Competencies",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectionReason",
                schema: "CoreSchema",
                table: "ReviewPeriods");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                schema: "CoreSchema",
                table: "Competencies");
        }
    }
}
