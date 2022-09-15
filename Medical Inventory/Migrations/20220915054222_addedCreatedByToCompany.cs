using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_Inventory.Migrations
{
    public partial class addedCreatedByToCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedById",
                table: "Company",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Company",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "UpdatedById",
                table: "Company",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTime",
                table: "Company",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Company_CreatedById",
                table: "Company",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Company_UpdatedById",
                table: "Company",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_AspNetUsers_CreatedById",
                table: "Company",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_AspNetUsers_UpdatedById",
                table: "Company",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_AspNetUsers_CreatedById",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_AspNetUsers_UpdatedById",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_CreatedById",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_UpdatedById",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "UpdatedTime",
                table: "Company");
        }
    }
}
