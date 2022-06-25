using DemoSeguridad.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoSeguridad.Data
{
    public class ApplicationDbContext : DbContext
    {
        readonly IConfiguration _configuration;
        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolesPermissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(_configuration.GetConnectionString("DbConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.FirstName).HasColumnName("First_name");
                entity.Property(e => e.LastName).HasColumnName("Last_name");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasOne(b => b.Author)
                      .WithMany(a => a.Books)
                      .HasForeignKey(b => b.AuthorId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.FirstName).HasColumnName("First_name");
                entity.Property(e => e.LastName).HasColumnName("Last_name");
                entity.Property(e => e.HashPassword).HasColumnName("Hash_password");
                entity.Property(e => e.HashSalt).HasColumnName("Hash_salt");
                entity.Property(e => e.RoleName).HasColumnName("Role_name");

                entity.HasOne(u => u.Role)
                      .WithMany(r => r.Users)
                      .HasForeignKey(u => u.RoleName)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.HasKey(rp => new { rp.RoleName, rp.PermissionName });

                entity.Property(e => e.RoleName).HasColumnName("Role_Name");
                entity.Property(e => e.PermissionName).HasColumnName("Permission_Name");

                entity.HasOne(rp => rp.Role)
                      .WithMany(r => r.RolePermissions)
                      .HasForeignKey(rp => rp.RoleName)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(rp => rp.Permission)
                      .WithMany(r => r.RolePermissions)
                      .HasForeignKey(rp => rp.PermissionName)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
