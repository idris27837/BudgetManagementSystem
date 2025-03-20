using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompetencyApp.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class ModifyStaffJobRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateRejected",
                schema: "CoreSchema",
                table: "StaffJobRoles",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                schema: "CoreSchema",
                table: "StaffJobRoles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HrdApprovedBy",
                schema: "CoreSchema",
                table: "StaffJobRoles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HrdDateApproved",
                schema: "CoreSchema",
                table: "StaffJobRoles",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "HrdDateRejected",
                schema: "CoreSchema",
                table: "StaffJobRoles",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "HrdIsApproved",
                schema: "CoreSchema",
                table: "StaffJobRoles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HrdIsRejected",
                schema: "CoreSchema",
                table: "StaffJobRoles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "HrdRejectedBy",
                schema: "CoreSchema",
                table: "StaffJobRoles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HrdRejectionReason",
                schema: "CoreSchema",
                table: "StaffJobRoles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                schema: "CoreSchema",
                table: "StaffJobRoles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RejectedBy",
                schema: "CoreSchema",
                table: "StaffJobRoles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                schema: "CoreSchema",
                table: "StaffJobRoles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoaResponse",
                schema: "CoreSchema",
                table: "StaffJobRoles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SoaStatus",
                schema: "CoreSchema",
                table: "StaffJobRoles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SupervisorId",
                schema: "CoreSchema",
                table: "StaffJobRoles",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateRejected",
                schema: "CoreSchema",
                table: "StaffJobRoles");

            migrationBuilder.DropColumn(
                name: "FullName",
                schema: "CoreSchema",
                table: "StaffJobRoles");

            migrationBuilder.DropColumn(
                name: "HrdApprovedBy",
                schema: "CoreSchema",
                table: "StaffJobRoles");

            migrationBuilder.DropColumn(
                name: "HrdDateApproved",
                schema: "CoreSchema",
                table: "StaffJobRoles");

            migrationBuilder.DropColumn(
                name: "HrdDateRejected",
                schema: "CoreSchema",
                table: "StaffJobRoles");

            migrationBuilder.DropColumn(
                name: "HrdIsApproved",
                schema: "CoreSchema",
                table: "StaffJobRoles");

            migrationBuilder.DropColumn(
                name: "HrdIsRejected",
                schema: "CoreSchema",
                table: "StaffJobRoles");

            migrationBuilder.DropColumn(
                name: "HrdRejectedBy",
                schema: "CoreSchema",
                table: "StaffJobRoles");

            migrationBuilder.DropColumn(
                name: "HrdRejectionReason",
                schema: "CoreSchema",
                table: "StaffJobRoles");

            migrationBuilder.DropColumn(
                name: "IsRejected",
                schema: "CoreSchema",
                table: "StaffJobRoles");

            migrationBuilder.DropColumn(
                name: "RejectedBy",
                schema: "CoreSchema",
                table: "StaffJobRoles");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                schema: "CoreSchema",
                table: "StaffJobRoles");

            migrationBuilder.DropColumn(
                name: "SoaResponse",
                schema: "CoreSchema",
                table: "StaffJobRoles");

            migrationBuilder.DropColumn(
                name: "SoaStatus",
                schema: "CoreSchema",
                table: "StaffJobRoles");

            migrationBuilder.DropColumn(
                name: "SupervisorId",
                schema: "CoreSchema",
                table: "StaffJobRoles");
        }
    }
}
