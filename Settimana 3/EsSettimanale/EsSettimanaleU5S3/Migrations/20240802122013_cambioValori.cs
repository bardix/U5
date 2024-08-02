using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EsSettimanaleU5S3.Migrations
{
    /// <inheritdoc />
    public partial class cambioValori : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 8, 2, 14, 20, 13, 334, DateTimeKind.Local).AddTicks(7603));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 8, 2, 14, 5, 27, 674, DateTimeKind.Local).AddTicks(6591));
        }
    }
}
