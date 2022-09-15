using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_Inventory.Migrations
{
    public partial class addedCreatedByToPatientGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedById",
                table: "PatientGroup",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "PatientGroup",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "UpdatedById",
                table: "PatientGroup",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTime",
                table: "PatientGroup",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_PatientGroup_CreatedById",
                table: "PatientGroup",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PatientGroup_UpdatedById",
                table: "PatientGroup",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientGroup_AspNetUsers_CreatedById",
                table: "PatientGroup",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientGroup_AspNetUsers_UpdatedById",
                table: "PatientGroup",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientGroup_AspNetUsers_CreatedById",
                table: "PatientGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientGroup_AspNetUsers_UpdatedById",
                table: "PatientGroup");

            migrationBuilder.DropIndex(
                name: "IX_PatientGroup_CreatedById",
                table: "PatientGroup");

            migrationBuilder.DropIndex(
                name: "IX_PatientGroup_UpdatedById",
                table: "PatientGroup");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "PatientGroup");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "PatientGroup");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "PatientGroup");

            migrationBuilder.DropColumn(
                name: "UpdatedTime",
                table: "PatientGroup");
        }
    }
}
