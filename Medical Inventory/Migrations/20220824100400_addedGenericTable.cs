using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_Inventory.Migrations
{
    public partial class addedGenericTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Generic",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "GenericId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Generic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generic", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_GenericId",
                table: "Products",
                column: "GenericId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Generic_GenericId",
                table: "Products",
                column: "GenericId",
                principalTable: "Generic",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Generic_GenericId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Generic");

            migrationBuilder.DropIndex(
                name: "IX_Products_GenericId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "GenericId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Generic",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
