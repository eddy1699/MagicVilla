using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNumberVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.CreateTable(
                name: "NumberVilla",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    VillaId = table.Column<int>(type: "int", nullable: false),
                    SpecialDetail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberVilla", x => x.VillaNo);
                    table.ForeignKey(
                        name: "FK_NumberVilla_Villas_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NumberVilla_VillaId",
                table: "NumberVilla",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumberVilla");

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Detail", "ImgUrl", "Name", "Ocuppants", "Price", "SquareMeters", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2023, 5, 3, 16, 45, 33, 894, DateTimeKind.Local).AddTicks(881), "Detalles", "", "Villa real", 123, 123.0, 123, new DateTime(2023, 5, 3, 16, 45, 33, 894, DateTimeKind.Local).AddTicks(926) },
                    { 2, "", new DateTime(2023, 5, 3, 16, 45, 33, 894, DateTimeKind.Local).AddTicks(929), "Detalles312", "", "Villa real312", 64, 567.0, 46, new DateTime(2023, 5, 3, 16, 45, 33, 894, DateTimeKind.Local).AddTicks(931) }
                });
        }
    }
}
