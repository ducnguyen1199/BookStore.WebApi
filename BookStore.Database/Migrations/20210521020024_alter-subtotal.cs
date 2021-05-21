using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Database.Migrations
{
    public partial class altersubtotal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "SubTotal",
                table: "BooksInCarts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "BooksInCarts");
        }
    }
}
