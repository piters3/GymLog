using Microsoft.EntityFrameworkCore.Migrations;

namespace GymLog.API.Migrations
{
    public partial class UserIntTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "Users",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<int>(
                name: "Height",
                table: "Users",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "Users",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "Height",
                table: "Users",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
