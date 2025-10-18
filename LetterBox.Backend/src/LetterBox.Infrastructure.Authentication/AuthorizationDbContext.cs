using LetterBox.Application.Accounts.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LetterBox.Infrastructure.Authentication
{
    public class AuthorizationDbContext(IConfiguration configuration)
        : IdentityDbContext<User, Role, Guid>
    {
        private const string DATABASE = "Database";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString(DATABASE));
            optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .ToTable("users");

            modelBuilder.Entity<User>()
                .Property(x => x.CreatedAt)
                .HasColumnName("created_at");

            modelBuilder.Entity<User>()
                .Property(x => x.isActive)
                .HasColumnName("is_active");

            modelBuilder.Entity<Role>()
                .ToTable("roles");

            modelBuilder.Entity<Permission>()
                .ToTable("permissions");

            modelBuilder.Entity<Permission>()
                .HasIndex(p => p.Code)
                .IsUnique();

            modelBuilder.Entity<Permission>()
                .Property(p => p.Description)
                .HasMaxLength(100)
                .HasColumnName("description");

            modelBuilder.Entity<RolePermission>()
                .ToTable("role_permissions");

            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(p => p.Permission)
                .WithMany()
                .HasForeignKey(rp => rp.PermissionId);

            modelBuilder.Entity<IdentityUserClaim<Guid>>()
                .ToTable("user_claims");

            modelBuilder.Entity<IdentityUserToken<Guid>>()
                .ToTable("user_tokens");

            modelBuilder.Entity<IdentityUserLogin<Guid>>()
                .ToTable("user_logins");

            modelBuilder.Entity<IdentityRoleClaim<Guid>>()
                .ToTable("role_claims");

            modelBuilder.Entity<IdentityUserRole<Guid>>()
                .ToTable("user_roles");
        }

        private ILoggerFactory CreateLoggerFactory() =>
            LoggerFactory.Create(builder => { builder.AddConsole(); });
    }
}
