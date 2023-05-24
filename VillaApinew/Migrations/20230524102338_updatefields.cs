using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaApinew.Migrations
{
    /// <inheritdoc />
    public partial class updatefields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 5, 24, 15, 53, 38, 763, DateTimeKind.Local).AddTicks(2599));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2023, 5, 24, 15, 53, 38, 763, DateTimeKind.Local).AddTicks(2615));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2023, 5, 24, 15, 53, 38, 763, DateTimeKind.Local).AddTicks(2616));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 5, 24, 15, 23, 47, 471, DateTimeKind.Local).AddTicks(4717));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2023, 5, 24, 15, 23, 47, 471, DateTimeKind.Local).AddTicks(4737));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2023, 5, 24, 15, 23, 47, 471, DateTimeKind.Local).AddTicks(4740));
        }
    }
}
