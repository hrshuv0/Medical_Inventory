using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_Inventory.Migrations
{
    public partial class removeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop ForeignKey

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Company_CompanyId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Generic_GenericId",
                table: "Products");

            



            // Drop Primary Key

            migrationBuilder.DropPrimaryKey(
                name:"PK_Products",
                table:"Products");

            migrationBuilder.DropPrimaryKey(
                name:"PK_Categories",
                table:"Categories");

            migrationBuilder.DropPrimaryKey(
                name:"PK_Generic",
                table:"Generic");

            migrationBuilder.DropPrimaryKey(
                name:"PK_Company",
                table:"Company");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Add primary key

            migrationBuilder.AddPrimaryKey(
                name:"PK_Products",
                table:"Products",
                column:"Id"
                );

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Company",
                table: "Company",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Generic",
                table: "Generic",
                column: "Id");


            // Add foreign key

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable:"Categories",
                principalColumn:"Id",
                onDelete:ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Company_CompanyId",
                table: "Products",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Generic_GenericId",
                table: "Products",
                column: "GenericId",
                principalTable: "Generic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            



        }
    }
}
