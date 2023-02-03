using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Migrations.JobPortalWeb
{
    public partial class V13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "seeker_id",
                table: "UserProfile",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_seeker_id",
                table: "UserProfile",
                column: "seeker_id",
                unique: true,
                filter: "[seeker_id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_Seeker_seeker_id",
                table: "UserProfile",
                column: "seeker_id",
                principalTable: "Seeker",
                principalColumn: "user_profile_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_Seeker_seeker_id",
                table: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_UserProfile_seeker_id",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "seeker_id",
                table: "UserProfile");

            migrationBuilder.AddForeignKey(
                name: "FK_Seeker_UserProfile_user_profile_id",
                table: "Seeker",
                column: "user_profile_id",
                principalTable: "UserProfile",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
