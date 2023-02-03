using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Migrations.JobPortalWeb
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobTypeActivity",
                columns: table => new
                {
                    job_type_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    job_post_id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("job_type_activity_pk", x => new { x.job_type_id, x.job_post_id });
                    table.ForeignKey(
                        name: "job_type_activity_job_post",
                        column: x => x.job_post_id,
                        principalTable: "JobPost",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "job_type_activity_job_type",
                        column: x => x.job_type_id,
                        principalTable: "JobType",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobTypeActivity_job_post_id",
                table: "JobTypeActivity",
                column: "job_post_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobTypeActivity");
        }
    }
}
