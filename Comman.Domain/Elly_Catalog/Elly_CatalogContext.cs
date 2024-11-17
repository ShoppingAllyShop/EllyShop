using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Comman.Domain.Elly_Catalog
{
    public partial class Elly_CatalogContext : DbContext
    {
        public Elly_CatalogContext()
        {
        }

        public Elly_CatalogContext(DbContextOptions<Elly_CatalogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; } = null!;
        public virtual DbSet<Collection> Collection { get; set; } = null!;
        public virtual DbSet<Color> Color { get; set; } = null!;
        public virtual DbSet<Guarantee> Guarantee { get; set; } = null!;
        public virtual DbSet<Guide> Guide { get; set; } = null!;
        public virtual DbSet<Product> Product { get; set; } = null!;
        public virtual DbSet<ProductDetail> ProductDetail { get; set; } = null!;
        public virtual DbSet<ProductImages> ProductImages { get; set; } = null!;
        public virtual DbSet<ProductTags> ProductTags { get; set; } = null!;
        public virtual DbSet<Rating> Rating { get; set; } = null!;
        public virtual DbSet<Size> Size { get; set; } = null!;
        public virtual DbSet<Tags> Tags { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-M6K1TNV0;Initial Catalog=Elly_Catalog;Persist Security Info=True;User ID=sa;Password=1;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Gender).HasComment("0:Woman,1:Men");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Collection>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ColorCode).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Guarantee>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Icon).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(255);
            });

            modelBuilder.Entity<Guide>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Discount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PercentDiscount).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Category");

                entity.HasOne(d => d.Collection)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CollectionId)
                    .HasConstraintName("FK_Product_Collection");
            });

            modelBuilder.Entity<ProductDetail>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ProductId).HasMaxLength(50);

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.ProductDetail)
                    .HasForeignKey(d => d.ColorId)
                    .HasConstraintName("FK_ProductDetail_Color");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductDetail)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductDetail_Product");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.ProductDetail)
                    .HasForeignKey(d => d.SizeId)
                    .HasConstraintName("FK_ProductDetail_Size");
            });

            modelBuilder.Entity<ProductImages>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DefaultColor).HasComment("dung de set mau mac dinh cho prodcutdetail");

                entity.Property(e => e.Picture).HasMaxLength(255);

                entity.Property(e => e.ProductId).HasMaxLength(50);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .HasComment("ProductColor: hình đại diện khi chọn màu sản phẩm");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.ColorId)
                    .HasConstraintName("FK_ProductImages_Color");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Image_Product1");
            });

            modelBuilder.Entity<ProductTags>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AssignedDate).HasColumnType("datetime");

                entity.Property(e => e.ProductId).HasMaxLength(50);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductTags)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductTags_Product");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.ProductTags)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductTags_Tags");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ProductId).HasMaxLength(50);
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Size1)
                    .HasMaxLength(50)
                    .HasColumnName("Size");

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<Tags>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.TypeName).HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
