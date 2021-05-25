using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Database.Migrations
{
    public partial class detailorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DetailOrders",
                table: "DetailOrders");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DetailOrders",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "DetailOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetailOrders",
                table: "DetailOrders",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetailOrders_IdBook",
                table: "DetailOrders",
                column: "IdBook");

            migrationBuilder.CreateIndex(
                name: "IX_DetailOrders_OrderId",
                table: "DetailOrders",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailOrders_Orders_OrderId",
                table: "DetailOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailOrders_Orders_OrderId",
                table: "DetailOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetailOrders",
                table: "DetailOrders");

            migrationBuilder.DropIndex(
                name: "IX_DetailOrders_IdBook",
                table: "DetailOrders");

            migrationBuilder.DropIndex(
                name: "IX_DetailOrders_OrderId",
                table: "DetailOrders");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DetailOrders");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "DetailOrders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetailOrders",
                table: "DetailOrders",
                columns: new[] { "IdBook", "IdOrder" });
        }
    }
}
