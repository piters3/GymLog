using Microsoft.EntityFrameworkCore.Migrations;

namespace GymLog.API.Migrations
{
    public partial class Version : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Workouts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Workouts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Muscle",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Muscle",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Exercises",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Exercises",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Equipments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Equipments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Daylogs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Daylogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Muscle");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Muscle");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Daylogs");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Daylogs");
        }
    }
}
