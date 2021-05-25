using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Database.Migrations
{
    public partial class fee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Surcharge",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

			migrationBuilder.AlterColumn<string>(
				name: "Avatar",
				table: "AspNetUsers",
				type: "nvarchar(max)",
				nullable: true,
				defaultValueSql: "https://localhost:44369/avatars/defaultAvatar.jpg",
				oldClrType: typeof(string),
				oldType: "nvarchar(max)",
				oldNullable: true,
				oldDefaultValueSql: "https://localhost:44369/avatars/defaultAvatar/jpg");
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Surcharge",
                table: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                defaultValueSql: "https://localhost:44369/avatars/defaultAvatar/jpg",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValueSql: "https://localhost:44369/avatars/defaultAvatar.jpg");
        }
    }
}
