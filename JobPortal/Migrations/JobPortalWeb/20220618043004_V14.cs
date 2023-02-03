using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Migrations.JobPortalWeb
{
    public partial class V14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "level",
                table: "Seeker");

            migrationBuilder.AlterColumn<string>(
                name: "introduction",
                table: "Seeker",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "ntext",
                oldNullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "city_id",
                table: "Company",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name_city = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("city_pk", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Level",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name_level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("level_pk", x => x.id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Level");

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

            migrationBuilder.AlterColumn<string>(
                name: "introduction",
                table: "Seeker",
                type: "ntext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "level",
                table: "Seeker",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
