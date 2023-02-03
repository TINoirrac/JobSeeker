﻿// <auto-generated />
using System;
using JobPortal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JobPortal.Migrations.JobPortalWeb
{
    [DbContext(typeof(JobPortalWebContext))]
    [Migration("20220618080838_V15")]
    partial class V15
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("JobPortal.Models.City", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("id");

                    b.Property<string>("NameCity")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("name_city");

                    b.HasKey("Id")
                        .HasName("city_pk");

                    b.ToTable("City", (string)null);
                });

            modelBuilder.Entity("JobPortal.Models.Company", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("id");

                    b.Property<string>("CityId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("city_id");

                    b.Property<string>("CompanyLogoUrl")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("company_logo_url");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("company_name");

                    b.Property<string>("CompanyWebsiteUrl")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("company_website_url");

                    b.Property<DateTime?>("EstablishmentDate")
                        .HasColumnType("date")
                        .HasColumnName("establishment_date");

                    b.Property<string>("ProfileDescription")
                        .IsUnicode(true)
                        .HasColumnType("ntext")
                        .HasColumnName("profile_description");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Company", (string)null);
                });

            modelBuilder.Entity("JobPortal.Models.JobPost", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("id");

                    b.Property<string>("CompanyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("company_id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("date")
                        .HasColumnName("created_date");

                    b.Property<string>("JobDescription")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("ntext")
                        .HasColumnName("job_description");

                    b.Property<string>("JobLocation")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("job_location");

                    b.Property<string>("NameJob")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("name_job");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("JobPost", (string)null);
                });

            modelBuilder.Entity("JobPortal.Models.JobPostActivity", b =>
                {
                    b.Property<string>("UserProfileId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("seeker_profile_id");

                    b.Property<string>("JobPostId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("job_post_id");

                    b.Property<DateTime>("ApplyDate")
                        .HasColumnType("datetime")
                        .HasColumnName("apply_date");

                    b.HasKey("UserProfileId", "JobPostId")
                        .HasName("job_post_activity_pk");

                    b.HasIndex("JobPostId");

                    b.ToTable("JobPostActivity", (string)null);
                });

            modelBuilder.Entity("JobPortal.Models.JobType", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("id");

                    b.Property<string>("JobType1")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("job_type");

                    b.HasKey("Id");

                    b.ToTable("JobType", (string)null);
                });

            modelBuilder.Entity("JobPortal.Models.JobTypeActivity", b =>
                {
                    b.Property<string>("JobTypeId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("job_type_id");

                    b.Property<string>("JobPostId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("job_post_id");

                    b.HasKey("JobTypeId", "JobPostId")
                        .HasName("job_type_activity_pk");

                    b.HasIndex("JobPostId");

                    b.ToTable("JobTypeActivity", (string)null);
                });

            modelBuilder.Entity("JobPortal.Models.Level", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("id");

                    b.Property<string>("NameLevel")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name_level");

                    b.HasKey("Id")
                        .HasName("level_pk");

                    b.ToTable("Level", (string)null);
                });

            modelBuilder.Entity("JobPortal.Models.Seeker", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("user_profile_id");

                    b.Property<string>("CityId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("city_id");

                    b.Property<string>("CvUrl")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("cv_url");

                    b.Property<string>("Introduction")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("introduction");

                    b.Property<string>("JobTitle")
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("job_title");

                    b.Property<string>("LevelId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("level_id");

                    b.Property<string>("YOfExp")
                        .HasMaxLength(20)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("year_of_exp");

                    b.HasKey("Id")
                        .HasName("seeker_pk");

                    b.HasIndex("CityId");

                    b.HasIndex("LevelId");

                    b.ToTable("Seeker", (string)null);
                });

            modelBuilder.Entity("JobPortal.Models.UserProfile", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("company_id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("date")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("full_name");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit")
                        .HasColumnName("gender");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SeekerId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("seeker_id");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserImageUrl")
                        .HasMaxLength(500)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("user_image_url");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId")
                        .IsUnique()
                        .HasFilter("[company_id] IS NOT NULL");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("SeekerId")
                        .IsUnique()
                        .HasFilter("[seeker_id] IS NOT NULL");

                    b.ToTable("UserProfile", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("JobPortal.Models.Company", b =>
                {
                    b.HasOne("JobPortal.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.Navigation("City");
                });

            modelBuilder.Entity("JobPortal.Models.JobPost", b =>
                {
                    b.HasOne("JobPortal.Models.Company", "Company")
                        .WithMany("JobPosts")
                        .HasForeignKey("CompanyId")
                        .IsRequired()
                        .HasConstraintName("job_post_company");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("JobPortal.Models.JobPostActivity", b =>
                {
                    b.HasOne("JobPortal.Models.JobPost", "JobPost")
                        .WithMany("JobPostActivities")
                        .HasForeignKey("JobPostId")
                        .IsRequired()
                        .HasConstraintName("job_post_activity_job_post");

                    b.HasOne("JobPortal.Models.UserProfile", "UserProfile")
                        .WithMany("JobPostActivities")
                        .HasForeignKey("UserProfileId")
                        .IsRequired()
                        .HasConstraintName("job_post_activity_seeker_profile");

                    b.Navigation("JobPost");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("JobPortal.Models.JobTypeActivity", b =>
                {
                    b.HasOne("JobPortal.Models.JobPost", "JobPost")
                        .WithMany("JobTypeActivities")
                        .HasForeignKey("JobPostId")
                        .IsRequired()
                        .HasConstraintName("job_type_activity_job_post");

                    b.HasOne("JobPortal.Models.JobType", "JobType")
                        .WithMany("JobTypeActivities")
                        .HasForeignKey("JobTypeId")
                        .IsRequired()
                        .HasConstraintName("job_type_activity_job_type");

                    b.Navigation("JobPost");

                    b.Navigation("JobType");
                });

            modelBuilder.Entity("JobPortal.Models.Seeker", b =>
                {
                    b.HasOne("JobPortal.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.HasOne("JobPortal.Models.Level", "Level")
                        .WithMany()
                        .HasForeignKey("LevelId");

                    b.Navigation("City");

                    b.Navigation("Level");
                });

            modelBuilder.Entity("JobPortal.Models.UserProfile", b =>
                {
                    b.HasOne("JobPortal.Models.Company", "Company")
                        .WithOne("UserProfile")
                        .HasForeignKey("JobPortal.Models.UserProfile", "CompanyId");

                    b.HasOne("JobPortal.Models.Seeker", "Seeker")
                        .WithOne("UserProfile")
                        .HasForeignKey("JobPortal.Models.UserProfile", "SeekerId");

                    b.Navigation("Company");

                    b.Navigation("Seeker");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("JobPortal.Models.UserProfile", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("JobPortal.Models.UserProfile", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobPortal.Models.UserProfile", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("JobPortal.Models.UserProfile", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JobPortal.Models.Company", b =>
                {
                    b.Navigation("JobPosts");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("JobPortal.Models.JobPost", b =>
                {
                    b.Navigation("JobPostActivities");

                    b.Navigation("JobTypeActivities");
                });

            modelBuilder.Entity("JobPortal.Models.JobType", b =>
                {
                    b.Navigation("JobTypeActivities");
                });

            modelBuilder.Entity("JobPortal.Models.Seeker", b =>
                {
                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("JobPortal.Models.UserProfile", b =>
                {
                    b.Navigation("JobPostActivities");
                });
#pragma warning restore 612, 618
        }
    }
}
