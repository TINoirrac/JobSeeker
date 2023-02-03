using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Migrations.JobPortalWeb
{
    public partial class V7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddForeignKey(
                name: "FK_JobPost_JobType_JobTypeId",
                table: "JobPost",
                column: "JobTypeId",
                principalTable: "JobType",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPost_JobType_JobTypeId",
                table: "JobPost");

            migrationBuilder.AlterColumn<string>(
                name: "JobTypeId",
                table: "JobPost",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JobPost_JobType_JobTypeId",
                table: "JobPost",
                column: "JobTypeId",
                principalTable: "JobType",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
