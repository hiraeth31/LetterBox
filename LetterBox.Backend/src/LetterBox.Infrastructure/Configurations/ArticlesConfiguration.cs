using LetterBox.Domain.ArticlesManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetterBox.Infrastructure.Configurations
{
    public class ArticlesConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("articles");

            builder.HasKey(a => a.Id);

            builder.Property(ar => ar.Title)
                .HasColumnName("title");

            builder.Property(ar => ar.Slug)
                .HasColumnName("slug");

            builder.Property(ar => ar.Content)
                .HasColumnName("content");

            builder.Property(ar => ar.Excerpt)
                .HasColumnName("excerpt");

            builder.Property(ar => ar.Status)
                .HasColumnName("status");

            builder.Property(ar => ar.FeaturedImage)
                .HasColumnName("feautured_image");

            builder.Property(ar => ar.Excerpt)
                .HasColumnName("excerpt");

            builder.Property(ar => ar.CreatedAt)
                .HasColumnName("created_at");

            builder.Property(ar => ar.UpdatedAt)
                .HasColumnName("updated_at");

            builder.HasOne(ar => ar.Category)
                .WithMany(c => c.Articles)
                .HasForeignKey(ar => ar.CategoryId);
        }
    }
}
