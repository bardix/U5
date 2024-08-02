using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsSettimanaleU5S3.Migrations
{
    /// <inheritdoc />
    public partial class CambioData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 8, 2, 9, 49, 4, 497, DateTimeKind.Local).AddTicks(2385));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 8, 1, 18, 39, 28, 202, DateTimeKind.Local).AddTicks(3853));
        }
    }
}
