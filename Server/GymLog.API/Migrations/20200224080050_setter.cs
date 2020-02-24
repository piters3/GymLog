using Microsoft.EntityFrameworkCore.Migrations;

namespace GymLog.API.Migrations
{
    public partial class setter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Exercises_ExerciseId1",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_ExerciseId1",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "ExerciseId1",
                table: "Workouts");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseId",
                table: "Workouts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_ExerciseId",
                table: "Workouts",
                column: "ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Exercises_ExerciseId",
                table: "Workouts",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Exercises_ExerciseId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_ExerciseId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "ExerciseId",
                table: "Workouts");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseId1",
                table: "Workouts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_ExerciseId1",
                table: "Workouts",
                column: "ExerciseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Exercises_ExerciseId1",
                table: "Workouts",
                column: "ExerciseId1",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
