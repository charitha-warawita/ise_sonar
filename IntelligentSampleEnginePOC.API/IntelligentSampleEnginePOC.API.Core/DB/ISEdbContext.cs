using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IntelligentSampleEnginePOC.API.Core.DB
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
        public virtual DbSet<Qualification> Qualifications { get; set; } = null!;
        public virtual DbSet<Quotum> Quota { get; set; } = null!;
        public virtual DbSet<TargetAudience> TargetAudiences { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=eo2dwdev03.corp.gmi.lcl;Database=ISEdb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.CintCurrentCostLink).HasMaxLength(500);

                entity.Property(e => e.CintSelfLink).HasMaxLength(500);

                entity.Property(e => e.CintTestingLink).HasMaxLength(500);

                entity.Property(e => e.LinkToSurvey).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(60);

                entity.Property(e => e.Reference).HasMaxLength(50);

                entity.Property(e => e.UserId).HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Project_User");
            });

            modelBuilder.Entity<Qualification>(entity =>
            {
                entity.ToTable("Qualification");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.LogicalOperator).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.PreCodes).HasMaxLength(200);

                entity.Property(e => e.TargetAudienceId).HasMaxLength(50);

                entity.HasOne(d => d.TargetAudience)
                    .WithMany(p => p.Qualifications)
                    .HasForeignKey(d => d.TargetAudienceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Qualification_TargetAudience");
            });

            modelBuilder.Entity<Quotum>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.QuotaName).HasMaxLength(200);

                entity.Property(e => e.TargetAudienceId).HasMaxLength(50);

                entity.HasOne(d => d.TargetAudience)
                    .WithMany(p => p.Quota)
                    .HasForeignKey(d => d.TargetAudienceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Quota_TargetAudience");
            });

            modelBuilder.Entity<TargetAudience>(entity =>
            {
                entity.ToTable("TargetAudience");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.LimitType).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.ProjectId).HasMaxLength(50);

                entity.Property(e => e.Tanumber).HasColumnName("TANumber");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.TargetAudiences)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TargetAudience_Project");
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
