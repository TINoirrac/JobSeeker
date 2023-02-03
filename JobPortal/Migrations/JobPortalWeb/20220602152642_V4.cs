using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Migrations.JobPortalWeb
{
    public partial class V4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "company_id",
                table: "SeekerProfile",
                type: "nvarchar(450)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_SeekerProfile_Company_company_id",
                table: "SeekerProfile",
                column: "company_id",
                principalTable: "Company",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeekerProfile_Company_company_id",
                table: "SeekerProfile");

            migrationBuilder.DropIndex(
                name: "IX_SeekerProfile_company_id",
                table: "SeekerProfile");

            migrationBuilder.DropColumn(
                name: "company_id",
                table: "SeekerProfile");
        }
    }
}
