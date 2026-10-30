using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductosYCategorias.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDatosCOP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 1, 5, 14, 30, 0, 0, DateTimeKind.Utc), 5499900m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 1, 7, 16, 0, 0, 0, DateTimeKind.Utc), 89900m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 1, 9, 20, 45, 0, 0, DateTimeKind.Utc), 249900m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 1, 12, 13, 15, 0, 0, DateTimeKind.Utc), 1499900m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 1, 15, 22, 20, 0, 0, DateTimeKind.Utc), 199900m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 1, 18, 15, 0, 0, 0, DateTimeKind.Utc), 79900m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 1, 20, 17, 30, 0, 0, DateTimeKind.Utc), 129900m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 1, 23, 14, 10, 0, 0, DateTimeKind.Utc), 189900m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 1, 26, 19, 5, 0, 0, DateTimeKind.Utc), 599900m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 1, 29, 21, 40, 0, 0, DateTimeKind.Utc), 349900m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 2, 2, 13, 0, 0, 0, DateTimeKind.Utc), 89900m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 2, 5, 16, 25, 0, 0, DateTimeKind.Utc), 149900m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 2, 8, 18, 50, 0, 0, DateTimeKind.Utc), 59900m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 2, 11, 15, 35, 0, 0, DateTimeKind.Utc), 1299900m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 2, 14, 20, 0, 0, 0, DateTimeKind.Utc), 249900m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 2, 17, 14, 45, 0, 0, DateTimeKind.Utc), 59900m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 2, 20, 17, 0, 0, 0, DateTimeKind.Utc), 49900m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 2, 23, 19, 30, 0, 0, DateTimeKind.Utc), 129900m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 2, 26, 13, 20, 0, 0, DateTimeKind.Utc), 24900m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 3, 1, 21, 10, 0, 0, DateTimeKind.Utc), 12900m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 1, 5, 9, 30, 0, 0, DateTimeKind.Utc), 1299.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 1, 7, 11, 0, 0, 0, DateTimeKind.Utc), 24.50m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 1, 9, 15, 45, 0, 0, DateTimeKind.Utc), 89.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 1, 12, 8, 15, 0, 0, DateTimeKind.Utc), 349.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 1, 15, 17, 20, 0, 0, DateTimeKind.Utc), 59.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 1, 18, 10, 0, 0, 0, DateTimeKind.Utc), 32.75m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 1, 20, 12, 30, 0, 0, DateTimeKind.Utc), 45.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 1, 23, 9, 10, 0, 0, DateTimeKind.Utc), 79.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 1, 26, 14, 5, 0, 0, DateTimeKind.Utc), 149.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 1, 29, 16, 40, 0, 0, DateTimeKind.Utc), 119.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 2, 2, 8, 0, 0, 0, DateTimeKind.Utc), 29.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 2, 5, 11, 25, 0, 0, DateTimeKind.Utc), 54.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 2, 8, 13, 50, 0, 0, DateTimeKind.Utc), 19.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 2, 11, 10, 35, 0, 0, DateTimeKind.Utc), 299.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 2, 14, 15, 0, 0, 0, DateTimeKind.Utc), 89.50m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 2, 17, 9, 45, 0, 0, DateTimeKind.Utc), 21.90m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 2, 20, 12, 0, 0, 0, DateTimeKind.Utc), 18.50m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 2, 23, 14, 30, 0, 0, DateTimeKind.Utc), 39.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 2, 26, 8, 20, 0, 0, DateTimeKind.Utc), 8.75m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "FechaCreacion", "Price" },
                values: new object[] { new DateTime(2026, 3, 1, 16, 10, 0, 0, DateTimeKind.Utc), 4.20m });
        }
    }
}
