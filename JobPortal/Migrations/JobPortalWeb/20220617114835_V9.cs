using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Migrations.JobPortalWeb
{
    public partial class V9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "job_post_activity_seeker_profile",
                table: "JobPostActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaims_SeekerProfile_UserId",
                table: "UserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_SeekerProfile_UserId",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_SeekerProfile_UserId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_SeekerProfile_UserId",
                table: "UserTokens");

            migrationBuilder.DropTable(
                name: "EducationDetail");

            migrationBuilder.DropTable(
                name: "ExperienceDetail");

            migrationBuilder.AlterColumn<DateTime>(
                name: "apply_date",
                table: "JobPostActivity",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.CreateTable(
                name: "Seeker",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    job_title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    year_of_exp = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    introduction = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("seeker_pk", x => x.id);
                });

            //migrationBuilder.CreateTable(
            //    name: "UserProfile",
            //    columns: table => new
            //    {
            //        id = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //        full_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        date_of_birth = table.Column<DateTime>(type: "date", nullable: true),
            //        gender = table.Column<bool>(type: "bit", nullable: true),
            //        user_image_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
            //        seeker_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
            //        SeekerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
            //        UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            //        EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
            //        PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
            //        TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
            //        LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
            //        LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
            //        AccessFailedCount = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserProfile", x => x.id);
            //        table.ForeignKey(
            //            name: "FK_UserProfile_Company_seeker_id",
            //            column: x => x.seeker_id,
            //            principalTable: "Company",
            //            principalColumn: "id");
            //        table.ForeignKey(
            //            name: "FK_UserProfile_Seeker_SeekerId",
            //            column: x => x.SeekerId,
            //            principalTable: "Seeker",
            //            principalColumn: "id");
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "EmailIndex",
            //    table: "UserProfile",
            //    column: "NormalizedEmail");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserProfile_seeker_id",
            //    table: "UserProfile",
            //    column: "seeker_id",
            //    unique: true,
            //    filter: "[seeker_id] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserProfile_SeekerId",
            //    table: "UserProfile",
            //    column: "SeekerId",
            //    unique: true,
            //    filter: "[SeekerId] IS NOT NULL");

            //migrationBuilder.CreateIndex(
            //    name: "UserNameIndex",
            //    table: "UserProfile",
            //    column: "NormalizedUserName",
            //    unique: true,
            //    filter: "[NormalizedUserName] IS NOT NULL");

            //migrationBuilder.AddForeignKey(
            //    name: "job_post_activity_seeker_profile",
            //    table: "JobPostActivity",
            //    column: "seeker_profile_id",
            //    principalTable: "UserProfile",
            //    principalColumn: "id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserClaims_UserProfile_UserId",
            //    table: "UserClaims",
            //    column: "UserId",
            //    principalTable: "UserProfile",
            //    principalColumn: "id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserLogins_UserProfile_UserId",
            //    table: "UserLogins",
            //    column: "UserId",
            //    principalTable: "UserProfile",
            //    principalColumn: "id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserRoles_UserProfile_UserId",
            //    table: "UserRoles",
            //    column: "UserId",
            //    principalTable: "UserProfile",
            //    principalColumn: "id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserTokens_UserProfile_UserId",
            //    table: "UserTokens",
            //    column: "UserId",
            //    principalTable: "UserProfile",
            //    principalColumn: "id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "job_post_activity_seeker_profile",
                table: "JobPostActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaims_UserProfile_UserId",
                table: "UserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_UserProfile_UserId",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_UserProfile_UserId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_UserProfile_UserId",
                table: "UserTokens");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "Seeker");

            migrationBuilder.AlterColumn<DateTime>(
                name: "apply_date",
                table: "JobPostActivity",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.CreateTable(
                name: "SeekerProfile",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    company_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date_of_birth = table.Column<DateTime>(type: "date", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    gender = table.Column<bool>(type: "bit", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    user_image_url = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeekerProfile", x => x.id);
                    table.ForeignKey(
                        name: "FK_SeekerProfile_Company_company_id",
                        column: x => x.company_id,
                        principalTable: "Company",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "EducationDetail",
                columns: table => new
                {
                    user_account_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    certificate_degree_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    major = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Institute_university_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("education_detail_pk", x => new { x.user_account_id, x.certificate_degree_name, x.major });
                    table.ForeignKey(
                        name: "educ_dtl_seeker_profile",
                        column: x => x.user_account_id,
                        principalTable: "SeekerProfile",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ExperienceDetail",
                columns: table => new
                {
                    user_account_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    start_date = table.Column<DateTime>(type: "date", nullable: false),
                    end_date = table.Column<DateTime>(type: "date", nullable: false),
                    company_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "varchar(4000)", unicode: false, maxLength: 4000, nullable: false),
                    job_title = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("experience_detail_pk", x => new { x.user_account_id, x.start_date, x.end_date });
                    table.ForeignKey(
                        name: "exp_dtl_seeker_profile",
                        column: x => x.user_account_id,
                        principalTable: "SeekerProfile",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "SeekerProfile",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_SeekerProfile_company_id",
                table: "SeekerProfile",
                column: "company_id",
                unique: true,
                filter: "[company_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "SeekerProfile",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "job_post_activity_seeker_profile",
                table: "JobPostActivity",
                column: "seeker_profile_id",
                principalTable: "SeekerProfile",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaims_SeekerProfile_UserId",
                table: "UserClaims",
                column: "UserId",
                principalTable: "SeekerProfile",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_SeekerProfile_UserId",
                table: "UserLogins",
                column: "UserId",
                principalTable: "SeekerProfile",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_SeekerProfile_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "SeekerProfile",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_SeekerProfile_UserId",
                table: "UserTokens",
                column: "UserId",
                principalTable: "SeekerProfile",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
