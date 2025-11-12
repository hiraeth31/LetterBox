using LetterBox.Application.Accounts.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LetterBox.Infrastructure.Authentication
{
    public class AccountsDbContext(IConfiguration configuration)
        : IdentityDbContext<User, Role, Guid>
    {
        private const string DATABASE = "Database";

        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<AdminAccount> AdminAccounts => Set<AdminAccount>();
        public DbSet<RefreshSession> RefreshSessions => Set<RefreshSession>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString(DATABASE));
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .ToTable("users");

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany()
                .UsingEntity<IdentityUserRole<Guid>>();

            modelBuilder.Entity<AdminAccount>()
                .ToTable("admin_accounts");

            modelBuilder.Entity<AdminAccount>()
                .HasOne(a => a.User)
                .WithOne()
                .HasForeignKey<AdminAccount>(a => a.UserId);

            modelBuilder.Entity<AdminAccount>()
                .Property(x => x.FullName)
                .HasColumnName("full_name");

            modelBuilder.Entity<User>()
                .Property(x => x.CreatedAt)
                .HasColumnName("created_at");

            modelBuilder.Entity<User>()
                .Property(x => x.isActive)
                .HasColumnName("is_active");

            modelBuilder.Entity<RefreshSession>()
                .ToTable("refresh_sessions");

            modelBuilder.Entity<RefreshSession>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Role>()
                .ToTable("roles");

            modelBuilder.Entity<Permission>()
                .ToTable("permissions");

            modelBuilder.Entity<Permission>()
                .HasIndex(p => p.Code)
                .IsUnique();

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

            modelBuilder.HasDefaultSchema("accounts");
        }

        private ILoggerFactory CreateLoggerFactory() =>
            LoggerFactory.Create(builder => { builder.AddConsole(); });
    }
}
