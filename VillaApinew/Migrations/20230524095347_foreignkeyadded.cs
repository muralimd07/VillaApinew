using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaApinew.Migrations
{
    /// <inheritdoc />
    public partial class foreignkeyadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VillaId",
                table: "Villanumber",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Villanumber_VillaId",
                table: "Villanumber",
                column: "VillaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Villanumber_Villas_VillaId",
                table: "Villanumber",
                column: "VillaId",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Villanumber_Villas_VillaId",
                table: "Villanumber");

            migrationBuilder.DropIndex(
                name: "IX_Villanumber_VillaId",
                table: "Villanumber");

            migrationBuilder.DropColumn(
                name: "VillaId",
                table: "Villanumber");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2023, 5, 24, 13, 12, 54, 757, DateTimeKind.Local).AddTicks(757));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2023, 5, 24, 13, 12, 54, 757, DateTimeKind.Local).AddTicks(770));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreateDate",
                value: new DateTime(2023, 5, 24, 13, 12, 54, 757, DateTimeKind.Local).AddTicks(771));
        }
    }
}
