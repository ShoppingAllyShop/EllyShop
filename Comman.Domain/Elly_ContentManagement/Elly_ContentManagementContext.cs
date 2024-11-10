using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Comman.Domain.Elly_ContentManagement
{
    public partial class Elly_ContentManagementContext : DbContext
    {
        public Elly_ContentManagementContext()
        {
        }

        public Elly_ContentManagementContext(DbContextOptions<Elly_ContentManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Branch> Branch { get; set; } = null!;
        public virtual DbSet<GeneralInfomation> GeneralInfomation { get; set; } = null!;
        public virtual DbSet<Navigation> Navigation { get; set; } = null!;
        public virtual DbSet<News> News { get; set; } = null!;
        public virtual DbSet<NewsMedia> NewsMedia { get; set; } = null!;
        public virtual DbSet<Policy> Policy { get; set; } = null!;
        public virtual DbSet<Prize> Prize { get; set; } = null!;
        public virtual DbSet<Silde> Silde { get; set; } = null!;
        public virtual DbSet<SocialMedia> SocialMedia { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-M6K1TNV0;Initial Catalog=Elly_ContentManagement;Persist Security Info=True;User ID=sa;Password=1;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.BranchName).HasMaxLength(255);

                entity.Property(e => e.CityName).HasMaxLength(50);

                entity.Property(e => e.Region)
                    .HasMaxLength(50)
                    .HasComment("Phân chia theo vùng");
            });

            modelBuilder.Entity<GeneralInfomation>(entity =>
            {
                entity.HasKey(e => e.Title);

                entity.Property(e => e.Title).HasMaxLength(255);
            });

            modelBuilder.Entity<Navigation>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.NavContent).HasColumnType("text");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.NewContent).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);
            });

            modelBuilder.Entity<NewsMedia>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.NewsMediaContent).HasComment("Content");

                entity.Property(e => e.Title).HasMaxLength(255);
            });

            modelBuilder.Entity<Policy>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.Url).HasMaxLength(500);
            });

            modelBuilder.Entity<Prize>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.Image)
                    .HasMaxLength(500)
                    .HasColumnName("image");

                entity.Property(e => e.NewContent).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);
            });

            modelBuilder.Entity<Silde>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Position).HasMaxLength(50);
            });

            modelBuilder.Entity<SocialMedia>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
