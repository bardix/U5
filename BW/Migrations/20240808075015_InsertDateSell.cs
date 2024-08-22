using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BW2_Team6.Migrations
{
    /// <inheritdoc />
    public partial class InsertDateSell : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateSell",
                table: "Sells",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateSell",
                table: "Sells");
        }
    }
}
