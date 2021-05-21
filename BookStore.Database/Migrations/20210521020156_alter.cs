using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Database.Migrations
{
    public partial class alter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Subtotal",
                table: "DetailOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subtotal",
                table: "DetailOrders");
        }
    }
}
