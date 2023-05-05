using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class FillTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Detail", "ImgUrl", "Name", "Ocuppants", "Price", "SquareMeters", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2023, 5, 3, 16, 45, 33, 894, DateTimeKind.Local).AddTicks(881), "Detalles", "", "Villa real", 123, 123.0, 123, new DateTime(2023, 5, 3, 16, 45, 33, 894, DateTimeKind.Local).AddTicks(926) },
                    { 2, "", new DateTime(2023, 5, 3, 16, 45, 33, 894, DateTimeKind.Local).AddTicks(929), "Detalles312", "", "Villa real312", 64, 567.0, 46, new DateTime(2023, 5, 3, 16, 45, 33, 894, DateTimeKind.Local).AddTicks(931) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
