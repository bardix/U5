using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BW2_Team6.Migrations
{
    /// <inheritdoc />
    public partial class ProductDrawer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrawerProduct");

            migrationBuilder.CreateTable(
                name: "DrawerProducts",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    DrawerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrawerProducts", x => new { x.ProductId, x.DrawerId });
                    table.ForeignKey(
                        name: "FK_DrawerProducts_Drawers_DrawerId",
                        column: x => x.DrawerId,
                        principalTable: "Drawers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrawerProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrawerProducts_DrawerId",
                table: "DrawerProducts",
                column: "DrawerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrawerProducts");

            migrationBuilder.CreateTable(
                name: "DrawerProduct",
                columns: table => new
                {
                    DrawersId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrawerProduct", x => new { x.DrawersId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_DrawerProduct_Drawers_DrawersId",
                        column: x => x.DrawersId,
                        principalTable: "Drawers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrawerProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrawerProduct_ProductId",
                table: "DrawerProduct",
                column: "ProductId");
        }
    }
}
