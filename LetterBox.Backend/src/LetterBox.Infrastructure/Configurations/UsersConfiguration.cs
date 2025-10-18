using LetterBox.Domain.UsersManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetterBox.Infrastructure.Configurations
{
    public class UsersConfiguration
    {

    }
    //public class UsersConfiguration : IEntityTypeConfiguration<User>
    //{
    //    public void Configure(EntityTypeBuilder<User> builder)
    //    {
    //        builder.ToTable("users");

    //        builder.HasKey(c => c.Id);

    //        builder.Property(builder => builder.UserName)
    //            .HasColumnName("user_name");

    //        builder.Property(builder => builder.Email)
    //            .HasColumnName("email");

    //        builder.Property(builder => builder.PasswordHash)
    //            .HasColumnName("password_hash");

    //        builder.Property(builder => builder.Role)
    //            .HasColumnName("role");

    //        builder.Property(builder => builder.CreatedAt)
    //            .HasColumnName("created_at");

    //        builder.Property(builder => builder.isActive)
    //            .HasColumnName("is_active");
    //    }
    //}
}
