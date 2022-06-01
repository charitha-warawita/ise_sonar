using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IntelligentSampleEnginePOC.API.DB
{
    public partial class ISEdbContext : DbContext
    {
        public ISEdbContext()
        {
        }

        public ISEdbContext(DbContextOptions<ISEdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<ProjectTargetAudience> ProjectTargetAudiences { get; set; } = null!;
        public virtual DbSet<Qualification> Qualifications { get; set; } = null!;
        public virtual DbSet<QuotaGroup> QuotaGroups { get; set; } = null!;
        public virtual DbSet<QuotaGroupQuotum> QuotaGroupQuota { get; set; } = null!;
        public virtual DbSet<Quotum> Quota { get; set; } = null!;
        public virtual DbSet<TargetAudience> TargetAudiences { get; set; } = null!;
        public virtual DbSet<TargetAudienceQualification> TargetAudienceQualifications { get; set; } = null!;
        public virtual DbSet<TargetAudienceQuotaGroup> TargetAudienceQuotaGroups { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=eo2dwdev03.corp.gmi.lcl;Database=ISEdb;Trusted_Connection=True;user id=MSWeb_AppUser;password=w)@6GYY4*M%#Wb5M;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.LinkToSurvey).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(60);

                entity.Property(e => e.Reference).HasMaxLength(50);

                entity.Property(e => e.UserId).HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Project_User");
            });

            modelBuilder.Entity<ProjectTargetAudience>(entity =>
            {
                entity.ToTable("ProjectTargetAudience");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.ProjectId).HasMaxLength(50);

                entity.Property(e => e.TargetAudienceId).HasMaxLength(50);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectTargetAudiences)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectTargetAudience_Project");

                entity.HasOne(d => d.TargetAudience)
                    .WithMany(p => p.ProjectTargetAudiences)
                    .HasForeignKey(d => d.TargetAudienceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProjectTargetAudience_TargetAudience");
            });

            modelBuilder.Entity<Qualification>(entity =>
            {
                entity.ToTable("Qualification");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.LogicalOperator).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.PreCodes).HasMaxLength(200);
            });

            modelBuilder.Entity<QuotaGroup>(entity =>
            {
                entity.ToTable("QuotaGroup");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<QuotaGroupQuotum>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.QuotaGroupId).HasMaxLength(50);

                entity.Property(e => e.QuotaId).HasMaxLength(50);

                entity.HasOne(d => d.QuotaGroup)
                    .WithMany(p => p.QuotaGroupQuota)
                    .HasForeignKey(d => d.QuotaGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuotaGroupQuota_QuotaGroup");

                entity.HasOne(d => d.Quota)
                    .WithMany(p => p.QuotaGroupQuota)
                    .HasForeignKey(d => d.QuotaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuotaGroupQuota_Quota");
            });

            modelBuilder.Entity<Quotum>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.PreCode).HasMaxLength(200);

                entity.Property(e => e.QuotaName).HasMaxLength(200);
            });

            modelBuilder.Entity<TargetAudience>(entity =>
            {
                entity.ToTable("TargetAudience");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.LimitType).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Tanumber).HasColumnName("TANumber");
            });

            modelBuilder.Entity<TargetAudienceQualification>(entity =>
            {
                entity.ToTable("TargetAudienceQualification");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.QualificationId).HasMaxLength(50);

                entity.Property(e => e.TargetAudienceId).HasMaxLength(50);

                entity.HasOne(d => d.Qualification)
                    .WithMany(p => p.TargetAudienceQualifications)
                    .HasForeignKey(d => d.QualificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TargetAudienceQualification_Qualification");

                entity.HasOne(d => d.TargetAudience)
                    .WithMany(p => p.TargetAudienceQualifications)
                    .HasForeignKey(d => d.TargetAudienceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TargetAudienceQualification_TargetAudience");
            });

            modelBuilder.Entity<TargetAudienceQuotaGroup>(entity =>
            {
                entity.ToTable("TargetAudienceQuotaGroup");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.QuotaGroupId).HasMaxLength(50);

                entity.Property(e => e.TargetAudienceId).HasMaxLength(50);

                entity.HasOne(d => d.QuotaGroup)
                    .WithMany(p => p.TargetAudienceQuotaGroups)
                    .HasForeignKey(d => d.QuotaGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TargetAudienceQuotaGroup_QuotaGroup");

                entity.HasOne(d => d.TargetAudience)
                    .WithMany(p => p.TargetAudienceQuotaGroups)
                    .HasForeignKey(d => d.TargetAudienceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TargetAudienceQuotaGroup_TargetAudience");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(300);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
