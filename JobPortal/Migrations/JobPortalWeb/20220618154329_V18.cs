using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Migrations.JobPortalWeb
{
    public partial class V18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPost_Level_LevelsId",
                table: "JobPost");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeker_City_CitysId",
                table: "Seeker");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeker_Level_LevelsId",
                table: "Seeker");

            migrationBuilder.DropIndex(
                name: "IX_Seeker_CitysId",
                table: "Seeker");

            migrationBuilder.DropIndex(
                name: "IX_Seeker_LevelsId",
                table: "Seeker");

            migrationBuilder.DropIndex(
                name: "IX_JobPost_LevelsId",
                table: "JobPost");

            migrationBuilder.DropColumn(
                name: "CitysId",
                table: "Seeker");

            migrationBuilder.DropColumn(
                name: "LevelsId",
                table: "Seeker");

            migrationBuilder.DropColumn(
                name: "LevelsId",
                table: "JobPost");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CitysId",
                table: "Seeker",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LevelsId",
                table: "Seeker",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LevelsId",
                table: "JobPost",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seeker_CitysId",
                table: "Seeker",
                column: "CitysId");

            migrationBuilder.CreateIndex(
                name: "IX_Seeker_LevelsId",
                table: "Seeker",
                column: "LevelsId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPost_LevelsId",
                table: "JobPost",
                column: "LevelsId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPost_Level_LevelsId",
                table: "JobPost",
                column: "LevelsId",
                principalTable: "Level",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seeker_City_CitysId",
                table: "Seeker",
                column: "CitysId",
                principalTable: "City",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seeker_Level_LevelsId",
                table: "Seeker",
                column: "LevelsId",
                principalTable: "Level",
                principalColumn: "id");
        }
    }
}
