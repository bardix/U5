using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BW2_Team6.Migrations
{
    /// <inheritdoc />
    public partial class AggiuntaFiscalCodeOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FiscalCode",
                table: "Owners",
                type: "char(16)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FiscalCode",
                table: "Owners");
        }
    }
}
