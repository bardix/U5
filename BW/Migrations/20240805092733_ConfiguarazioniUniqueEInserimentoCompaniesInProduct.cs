using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BW2_Team6.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguarazioniUniqueEInserimentoCompaniesInProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberLockeer",
                table: "Locker",
                newName: "NumberLocker");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sells_NumberOfRecipe",
                table: "Sells",
                column: "NumberOfRecipe",
                unique: true,
                filter: "[NumberOfRecipe] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CompanyId",
                table: "Products",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Locker_NumberLocker",
                table: "Locker",
                column: "NumberLocker",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_Microchip",
                table: "Animals",
                column: "Microchip",
                unique: true,
                filter: "[Microchip] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Companies_CompanyId",
                table: "Products",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Companies_CompanyId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Sells_NumberOfRecipe",
                table: "Sells");

            migrationBuilder.DropIndex(
                name: "IX_Products_CompanyId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Locker_NumberLocker",
                table: "Locker");

            migrationBuilder.DropIndex(
                name: "IX_Animals_Microchip",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "NumberLocker",
                table: "Locker",
                newName: "NumberLockeer");
        }
    }
}
