using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_Inventory.Migrations
{
    public partial class addRecommandedPatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecommandedPatient",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    PatientGroupId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommandedPatient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecommandedPatient_PatientGroup_PatientGroupId",
                        column: x => x.PatientGroupId,
                        principalTable: "PatientGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecommandedPatient_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecommandedPatient_PatientGroupId",
                table: "RecommandedPatient",
                column: "PatientGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RecommandedPatient_ProductId",
                table: "RecommandedPatient",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecommandedPatient");
        }
    }
}
