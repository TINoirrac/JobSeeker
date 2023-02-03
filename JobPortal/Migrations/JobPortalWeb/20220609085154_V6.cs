using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Migrations.JobPortalWeb
{
    public partial class V6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "job_type_id",
                table: "JobPost",
                newName: "JobTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_JobPost_job_type_id",
                table: "JobPost",
                newName: "IX_JobPost_JobTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPost_JobType_JobTypeId",
                table: "JobPost",
                column: "JobTypeId",
                principalTable: "JobType",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPost_JobType_JobTypeId",
                table: "JobPost");

            migrationBuilder.RenameColumn(
                name: "JobTypeId",
                table: "JobPost",
                newName: "job_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_JobPost_JobTypeId",
                table: "JobPost",
                newName: "IX_JobPost_job_type_id");

            migrationBuilder.AddForeignKey(
                name: "job_post_job_type",
                table: "JobPost",
                column: "job_type_id",
                principalTable: "JobType",
                principalColumn: "id");
        }
    }
}
