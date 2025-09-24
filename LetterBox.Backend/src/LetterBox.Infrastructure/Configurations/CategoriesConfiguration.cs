using LetterBox.Domain.ArticlesManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetterBox.Infrastructure.Configurations
{
    public class CategoriesConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("categories");

            builder.HasKey(c => c.Id);

            builder.Property(builder => builder.Name)
                .HasColumnName("name");

            builder.Property(builder => builder.Slug)
                .HasColumnName("slug");

            builder.Property(builder => builder.Description)
                .HasColumnName("description");

            builder.Property(builder => builder.CreatedAt)
                .HasColumnName("created_at");
        }
    }
}
