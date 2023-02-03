using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Migrations.JobPortalWeb
{
    public partial class v11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_Company_seeker_id",
                table: "UserProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_Seeker_SeekerId",
                table: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_UserProfile_SeekerId",
                table: "UserProfile");

            migrationBuilder.RenameColumn(
                name: "SeekerId",
                table: "UserProfile",
                newName: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_company_id",
                table: "UserProfile",
                column: "company_id",
                unique: true,
                filter: "[company_id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_Company_company_id",
                table: "UserProfile",
                column: "company_id",
                principalTable: "Company",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_Seeker_seeker_id",
                table: "UserProfile",
                column: "seeker_id",
                principalTable: "Seeker",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_Company_company_id",
                table: "UserProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_Seeker_seeker_id",
                table: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_UserProfile_company_id",
                table: "UserProfile");

            migrationBuilder.RenameColumn(
                name: "company_id",
                table: "UserProfile",
                newName: "SeekerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_SeekerId",
                table: "UserProfile",
                column: "SeekerId",
                unique: true,
                filter: "[SeekerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_Company_seeker_id",
                table: "UserProfile",
                column: "seeker_id",
                principalTable: "Company",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_Seeker_SeekerId",
                table: "UserProfile",
                column: "SeekerId",
                principalTable: "Seeker",
                principalColumn: "id");
        }
    }
}
