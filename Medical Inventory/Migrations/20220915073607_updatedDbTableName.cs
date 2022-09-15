using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_Inventory.Migrations
{
    public partial class updatedDbTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_CreatedById",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_UpdatedById",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_CreatedById",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UpdatedById",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Company_CompanyId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Generic_GenericId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_RecommandedPatient_Products_ProductId",
                table: "RecommandedPatient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_Products_UpdatedById",
                table: "Product",
                newName: "IX_Product_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Products_GenericId",
                table: "Product",
                newName: "IX_Product_GenericId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CreatedById",
                table: "Product",
                newName: "IX_Product_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CompanyId",
                table: "Product",
                newName: "IX_Product_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "Product",
                newName: "IX_Product_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_UpdatedById",
                table: "Category",
                newName: "IX_Category_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_CreatedById",
                table: "Category",
                newName: "IX_Category_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_AspNetUsers_CreatedById",
                table: "Category",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_AspNetUsers_UpdatedById",
                table: "Category",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_AspNetUsers_CreatedById",
                table: "Product",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_AspNetUsers_UpdatedById",
                table: "Product",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Company_CompanyId",
                table: "Product",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Generic_GenericId",
                table: "Product",
                column: "GenericId",
                principalTable: "Generic",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecommandedPatient_Product_ProductId",
                table: "RecommandedPatient",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_AspNetUsers_CreatedById",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_AspNetUsers_UpdatedById",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_AspNetUsers_CreatedById",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_AspNetUsers_UpdatedById",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Company_CompanyId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Generic_GenericId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_RecommandedPatient_Product_ProductId",
                table: "RecommandedPatient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_Product_UpdatedById",
                table: "Products",
                newName: "IX_Products_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Product_GenericId",
                table: "Products",
                newName: "IX_Products_GenericId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CreatedById",
                table: "Products",
                newName: "IX_Products_CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CompanyId",
                table: "Products",
                newName: "IX_Products_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CategoryId",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_UpdatedById",
                table: "Categories",
                newName: "IX_Categories_UpdatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Category_CreatedById",
                table: "Categories",
                newName: "IX_Categories_CreatedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_CreatedById",
                table: "Categories",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_UpdatedById",
                table: "Categories",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_CreatedById",
                table: "Products",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UpdatedById",
                table: "Products",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Company_CompanyId",
                table: "Products",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Generic_GenericId",
                table: "Products",
                column: "GenericId",
                principalTable: "Generic",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RecommandedPatient_Products_ProductId",
                table: "RecommandedPatient",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
