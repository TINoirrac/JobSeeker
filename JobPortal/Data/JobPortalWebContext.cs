using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using JobPortal.Models;

namespace JobPortal.Models
{
    public partial class JobPortalWebContext : IdentityDbContext<UserProfile>
    {
        public JobPortalWebContext()
        {
        }

        public JobPortalWebContext(DbContextOptions<JobPortalWebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Seeker> Seeker { get; set; } = null!;
        public virtual DbSet<JobPost> JobPosts { get; set; } = null!;
        public virtual DbSet<JobPostActivity> JobPostActivities { get; set; } = null!;
        public virtual DbSet<JobType> JobTypes { get; set; } = null!;
        public virtual DbSet<JobTypeActivity> JobTypeActivity { get; set; } = null!;
        public virtual DbSet<UserProfile> UserProfiles { get; set; } = null!;
        public virtual DbSet<Level> Levels { get; set; } = null!;
        public virtual DbSet<City> Citys { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=DESKTOP-6URSO78\\SIUSERVER;Initial Catalog=JobPortalWeb;Integrated Security=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
                modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CompanyLogoUrl)
                    .HasMaxLength(500)
                    .IsUnicode(true)
                    .HasColumnName("company_logo_url");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(100)
                    .IsUnicode(true)
                    .HasColumnName("company_name");

                entity.Property(e => e.CompanyWebsiteUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("company_website_url");

                entity.Property(e => e.EstablishmentDate)
                    .HasColumnType("date")
                    .HasColumnName("establishment_date");

                entity.Property(e => e.ProfileDescription)
                    .HasColumnType("ntext")
                    .IsUnicode(true)
                    .HasColumnName("profile_description");

                entity.Property(e => e.Location)
                    .HasColumnType("ntext")
                    .IsUnicode(true)
                    .HasColumnName("location");

                entity.Property(e => e.City)
                    .HasColumnName("city");
            });

            modelBuilder.Entity<Seeker>(entity =>
            {
                entity.ToTable("Seeker");

                entity.HasKey(e => e.Id)
                .HasName("seeker_pk");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("user_profile_id");

                entity.Property(e => e.YOfExp)
                    .HasMaxLength(20)
                    .IsUnicode(true)
                    .HasColumnName("year_of_exp");

                entity.Property(e => e.JobTitle)
                    .HasMaxLength(50)
                    .IsUnicode(true)
                    .HasColumnName("job_title");

                entity.Property(e => e.Introduction)
                    .HasColumnName("introduction");

                entity.Property(e => e.City)
                    .HasColumnName("city");

                entity.Property(e => e.Level)
                    .HasColumnName("level");
                entity.Property(e => e.CvUrl)
                    .HasColumnName("cv_url");
            });


            modelBuilder.Entity<Level>(entity =>
            {
                entity.ToTable("Level");

                entity.HasKey(e => e.Id)
                    .HasName("level_pk");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.HasIndex(e=>e.NameLevel)
                .IsUnique();

                entity.Property(e => e.NameLevel)
                    .HasMaxLength(50)
                    .IsUnicode(true)
                    .HasColumnName("name_level");
            });


            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.HasKey(e => e.Id)
                    .HasName("city_pk");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");
                entity.HasIndex(e => e.NameCity)
                    .IsUnique();
                entity.Property(e => e.NameCity)
                    .HasMaxLength(20)
                    .IsUnicode(true)
                    .HasColumnName("name_city");
            });

            modelBuilder.Entity<JobPost>(entity =>
            {
                entity.ToTable("JobPost");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.NameJob)
                    .HasMaxLength(500)
                    .IsUnicode(true)
                    .HasColumnName("name_job");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("created_date");

                entity.Property(e => e.JobDescription)
                    .HasColumnType("ntext")
                    .IsUnicode(true)
                    .HasColumnName("job_description");

                entity.Property(e => e.Level)
                    .IsUnicode(true)
                    .HasColumnName("level");

                entity.Property(e => e.EmploymentType)
                    .IsUnicode(true)
                    .HasColumnName("employment_type");

                entity.Property(e => e.Status)
                    .HasColumnName("status");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.JobPosts)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("job_post_company");

            });

            modelBuilder.Entity<JobPostActivity>(entity =>
            {
                entity.HasKey(e => new { e.UserProfileId, e.JobPostId })
                    .HasName("job_post_activity_pk");

                entity.ToTable("JobPostActivity");

                entity.Property(e => e.UserProfileId).HasColumnName("seeker_profile_id");

                entity.Property(e => e.JobPostId).HasColumnName("job_post_id");

                entity.Property(e => e.ApplyDate)
                    .HasColumnType("datetime")
                    .HasColumnName("apply_date");

                entity.HasOne(d => d.JobPost)
                    .WithMany(p => p.JobPostActivities)
                    .HasForeignKey(d => d.JobPostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("job_post_activity_job_post");

                entity.HasOne(d => d.UserProfile)
                    .WithMany(p => p.JobPostActivities)
                    .HasForeignKey(d => d.UserProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("job_post_activity_seeker_profile");
            });

            modelBuilder.Entity<JobType>(entity =>
            {
                entity.ToTable("JobType");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.JobType1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("job_type");
            });

            modelBuilder.Entity<JobTypeActivity>(entity =>
            {
                entity.HasKey(e => new { e.JobTypeId, e.JobPostId })
                    .HasName("job_type_activity_pk");

                entity.ToTable("JobTypeActivity");

                entity.Property(e => e.JobTypeId).HasColumnName("job_type_id");

                entity.Property(e => e.JobPostId).HasColumnName("job_post_id");

                entity.HasOne(d => d.JobPost)
                    .WithMany(p => p.JobTypeActivities)
                    .HasForeignKey(d => d.JobPostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("job_type_activity_job_post");

                entity.HasOne(d => d.JobType)
                    .WithMany(p => p.JobTypeActivities)
                    .HasForeignKey(d => d.JobTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("job_type_activity_job_type");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.ToTable("UserProfile");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("date_of_birth");

                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .HasColumnName("full_name");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.UserImageUrl)
                    .HasMaxLength(500)
                    .IsUnicode(true)
                    .HasColumnName("user_image_url");

                entity.Property(e => e.CompanyId)
                    .HasColumnName("company_id");
                entity.Property(e => e.SeekerId)
                    .HasColumnName("seeker_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
