using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductosYCategorias.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Dispositivos electrónicos y accesorios", "Tecnología" },
                    { 2, "Artículos para el hogar y decoración", "Hogar" },
                    { 3, "Equipamiento deportivo y fitness", "Deportes" },
                    { 4, "Libros y material de lectura", "Libros" },
                    { 5, "Productos alimenticios y bebidas", "Alimentos" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "FechaCreacion", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 1, 5, 9, 30, 0, 0, DateTimeKind.Utc), "Laptop Pro 14", 1299.99m, 12 },
                    { 2, 1, new DateTime(2026, 1, 7, 11, 0, 0, 0, DateTimeKind.Utc), "Mouse Inalámbrico", 24.50m, 80 },
                    { 3, 1, new DateTime(2026, 1, 9, 15, 45, 0, 0, DateTimeKind.Utc), "Teclado Mecánico", 89.90m, 45 },
                    { 4, 1, new DateTime(2026, 1, 12, 8, 15, 0, 0, DateTimeKind.Utc), "Monitor 27\" 4K", 349.00m, 18 },
                    { 5, 1, new DateTime(2026, 1, 15, 17, 20, 0, 0, DateTimeKind.Utc), "Auriculares Bluetooth", 59.99m, 60 },
                    { 6, 2, new DateTime(2026, 1, 18, 10, 0, 0, 0, DateTimeKind.Utc), "Lámpara de Escritorio", 32.75m, 25 },
                    { 7, 2, new DateTime(2026, 1, 20, 12, 30, 0, 0, DateTimeKind.Utc), "Juego de Sábanas", 45.00m, 30 },
                    { 8, 2, new DateTime(2026, 1, 23, 9, 10, 0, 0, DateTimeKind.Utc), "Cafetera de Goteo", 79.90m, 15 },
                    { 9, 2, new DateTime(2026, 1, 26, 14, 5, 0, 0, DateTimeKind.Utc), "Aspiradora Compacta", 149.99m, 8 },
                    { 10, 2, new DateTime(2026, 1, 29, 16, 40, 0, 0, DateTimeKind.Utc), "Set de Ollas", 119.00m, 10 },
                    { 11, 3, new DateTime(2026, 2, 2, 8, 0, 0, 0, DateTimeKind.Utc), "Balón de Fútbol", 29.99m, 50 },
                    { 12, 3, new DateTime(2026, 2, 5, 11, 25, 0, 0, DateTimeKind.Utc), "Mancuernas 10 kg", 54.90m, 22 },
                    { 13, 3, new DateTime(2026, 2, 8, 13, 50, 0, 0, DateTimeKind.Utc), "Esterilla de Yoga", 19.99m, 70 },
                    { 14, 3, new DateTime(2026, 2, 11, 10, 35, 0, 0, DateTimeKind.Utc), "Bicicleta Estática", 299.00m, 6 },
                    { 15, 3, new DateTime(2026, 2, 14, 15, 0, 0, 0, DateTimeKind.Utc), "Raqueta de Tenis", 89.50m, 14 },
                    { 16, 4, new DateTime(2026, 2, 17, 9, 45, 0, 0, DateTimeKind.Utc), "Cien Años de Soledad", 21.90m, 40 },
                    { 17, 4, new DateTime(2026, 2, 20, 12, 0, 0, 0, DateTimeKind.Utc), "El Quijote", 18.50m, 35 },
                    { 18, 4, new DateTime(2026, 2, 23, 14, 30, 0, 0, DateTimeKind.Utc), "Clean Code", 39.99m, 20 },
                    { 19, 5, new DateTime(2026, 2, 26, 8, 20, 0, 0, DateTimeKind.Utc), "Café Molido 500 g", 8.75m, 100 },
                    { 20, 5, new DateTime(2026, 3, 1, 16, 10, 0, 0, DateTimeKind.Utc), "Chocolate Negro 70%", 4.20m, 120 }
                });

            migrationBuilder.CreateIndex(
                name: "UX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
