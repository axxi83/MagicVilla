using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class PopulatingVillaTableWithData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Detalle de la villa", new DateTime(2023, 6, 2, 16, 28, 41, 626, DateTimeKind.Local).AddTicks(3416), new DateTime(2023, 6, 2, 16, 28, 41, 626, DateTimeKind.Local).AddTicks(3452), "", 50, "Villa Real", 5, 200.0 },
                    { 2, "", "Detalle de la villa", new DateTime(2023, 6, 2, 16, 28, 41, 626, DateTimeKind.Local).AddTicks(3456), new DateTime(2023, 6, 2, 16, 28, 41, 626, DateTimeKind.Local).AddTicks(3457), "", 60, "Villa Brasil", 4, 250.0 }
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
