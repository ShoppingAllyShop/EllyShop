using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Comman.Domain.Elly_User
{
    public partial class Elly_UserContext : DbContext
    {
        public Elly_UserContext()
        {
        }

        public Elly_UserContext(DbContextOptions<Elly_UserContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Department { get; set; } = null!;
        public virtual DbSet<Employees> Employees { get; set; } = null!;
        public virtual DbSet<Permissions> Permissions { get; set; } = null!;
        public virtual DbSet<Position> Position { get; set; } = null!;
        public virtual DbSet<RefreshTokens> RefreshTokens { get; set; } = null!;
        public virtual DbSet<RolePermissions> RolePermissions { get; set; } = null!;
        public virtual DbSet<Roles> Roles { get; set; } = null!;
        public virtual DbSet<Users> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-TIJR1EN;Initial Catalog=Elly_User;Integrated Security=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasComment("Uniqueidentifier department id.");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("The time the record was created.");

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasComment("Name of department");

                entity.Property(e => e.IsActive).HasComment("The department is active or not (1: active, 0: inactive).");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("The last time of the record was updated.");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasIndex(e => e.DepartmentId, "Unique_DepartmentId")
                    .IsUnique();

                entity.HasIndex(e => e.PositionId, "Unique_PositionId")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "Unique_UserId")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Uniqueidentifier employee id.");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("The time the record was created.");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasComment("The date of birth");

                entity.Property(e => e.DepartmentId).HasComment("Link to department table.");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .HasComment("The full name.");

                entity.Property(e => e.Gender).HasComment("Gender - 0: Nam, 1: Nữ");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasComment("Phone number.");

                entity.Property(e => e.PositionId).HasComment("Link to position table.");

                entity.Property(e => e.Salary)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("Salary.");

                entity.Property(e => e.StartedDate)
                    .HasColumnType("date")
                    .HasComment("The started date.");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasComment("Status: Full-time, Resigned, Probation");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("The last time of the record was updated.");

                entity.Property(e => e.UserId).HasComment("Link to user table.");

                entity.HasOne(d => d.Department)
                    .WithOne(p => p.Employees)
                    .HasForeignKey<Employees>(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_Department");

                entity.HasOne(d => d.Position)
                    .WithOne(p => p.Employees)
                    .HasForeignKey<Employees>(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_Position");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Employees)
                    .HasForeignKey<Employees>(d => d.UserId)
                    .HasConstraintName("FK_Employees_Users");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasComment("Uniqueidentifier position id.");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasComment("The time the record was created.");

                entity.Property(e => e.PositionName)
                    .HasMaxLength(50)
                    .HasComment("Name of position.");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasComment("The last time of the record was updated.");
            });

            modelBuilder.Entity<Permissions>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<RefreshTokens>(entity =>
            {
                entity.HasIndex(e => e.Token, "UQ__RefreshT__1EB4F817866784F0")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Expiry).HasColumnType("datetime");

                entity.Property(e => e.Token).HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_RefreshTokens_Users");
            });

            modelBuilder.Entity<RolePermissions>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("FK_RolePermissions_Permissions");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_RolePermissions_Roles");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160EEA28F38")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.Email, "IX_Users_Email")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.PasswordHash).HasMaxLength(255);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ProfilePicture).HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
