using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Database.Migrations
{
    public partial class total : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "Orders");
        }
    }
}
