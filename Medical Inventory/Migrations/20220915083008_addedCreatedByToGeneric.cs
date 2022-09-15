using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_Inventory.Migrations
{
    public partial class addedCreatedByToGeneric : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedById",
                table: "Generic",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Generic",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "UpdatedById",
                table: "Generic",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTime",
                table: "Generic",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Generic_CreatedById",
                table: "Generic",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Generic_UpdatedById",
                table: "Generic",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Generic_AspNetUsers_CreatedById",
                table: "Generic",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Generic_AspNetUsers_UpdatedById",
                table: "Generic",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Generic_AspNetUsers_CreatedById",
                table: "Generic");

            migrationBuilder.DropForeignKey(
                name: "FK_Generic_AspNetUsers_UpdatedById",
                table: "Generic");

            migrationBuilder.DropIndex(
                name: "IX_Generic_CreatedById",
                table: "Generic");

            migrationBuilder.DropIndex(
                name: "IX_Generic_UpdatedById",
                table: "Generic");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Generic");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Generic");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Generic");

            migrationBuilder.DropColumn(
                name: "UpdatedTime",
                table: "Generic");
        }
    }
}
