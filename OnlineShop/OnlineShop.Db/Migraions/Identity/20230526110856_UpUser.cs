using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.Db.Migraions.Identity
{
    public partial class UpUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedPassword",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalizedPassword",
                table: "AspNetUsers");
        }
    }
}
