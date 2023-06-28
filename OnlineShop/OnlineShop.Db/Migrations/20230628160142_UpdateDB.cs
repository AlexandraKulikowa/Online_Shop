using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Db.Migrations
{
    public partial class UpdateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Genre", "IsPromo", "Name", "PaintingTechnique", "SizeId", "Year" },
                values: new object[] { 10, 12000m, "Олени на водопое", 3, true, "Картина \"Утро\"", "масло", 1, 2023 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Genre", "IsPromo", "Name", "PaintingTechnique", "SizeId", "Year" },
                values: new object[] { 11, 6000m, "Пейзаж со струящейся рекой", 0, true, "Картина \"Лето\"", "масло", 1, 2023 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Genre", "IsPromo", "Name", "PaintingTechnique", "SizeId", "Year" },
                values: new object[] { 12, 10000m, "Картина в кабинет", 2, false, "Картина \"Мудрость\"", "масло", 5, 2023 });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "Path", "ProductId" },
                values: new object[] { 10, "/images/Morning.jpg", 10 });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "Path", "ProductId" },
                values: new object[] { 11, "/images/Early.jpg", 11 });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "Path", "ProductId" },
                values: new object[] { 12, "/images/Past.jpg", 12 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);
        }
    }
}
