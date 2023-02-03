using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Migrations.JobPortalWeb
{
    public partial class V16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_City_city_id",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeker_City_city_id",
                table: "Seeker");

            migrationBuilder.DropForeignKey(
                name: "FK_Seeker_Level_level_id",
                table: "Seeker");

            migrationBuilder.DropIndex(
                name: "IX_Seeker_city_id",
                table: "Seeker");

            migrationBuilder.DropIndex(
                name: "IX_Seeker_level_id",
                table: "Seeker");

            migrationBuilder.DropIndex(
                name: "IX_Company_city_id",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "city_id",
                table: "Seeker");

            migrationBuilder.DropColumn(
                name: "level_id",
                table: "Seeker");

            migrationBuilder.DropColumn(
                name: "city_id",
                table: "Company");

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Seeker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "level",
                table: "Seeker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "company_logo_url",
                table: "Company",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldUnicode: false,
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Company",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "city",
                table: "Seeker");

            migrationBuilder.DropColumn(
                name: "level",
                table: "Seeker");

            migrationBuilder.DropColumn(
                name: "city",
                table: "Company");

            migrationBuilder.AddColumn<string>(
                name: "city_id",
                table: "Seeker",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "level_id",
                table: "Seeker",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "company_logo_url",
                table: "Company",
                type: "varchar(500)",
                unicode: false,
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city_id",
                table: "Company",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seeker_city_id",
                table: "Seeker",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_Seeker_level_id",
                table: "Seeker",
                column: "level_id");

            migrationBuilder.CreateIndex(
                name: "IX_Company_city_id",
                table: "Company",
                column: "city_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_City_city_id",
                table: "Company",
                column: "city_id",
                principalTable: "City",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seeker_City_city_id",
                table: "Seeker",
                column: "city_id",
                principalTable: "City",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seeker_Level_level_id",
                table: "Seeker",
                column: "level_id",
                principalTable: "Level",
                principalColumn: "id");
        }
    }
}
