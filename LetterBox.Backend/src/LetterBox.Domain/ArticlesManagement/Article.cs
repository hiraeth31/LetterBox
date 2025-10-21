namespace LetterBox.Domain.ArticlesManagement
{
    public sealed class Article
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; } = default!;
        public string Content { get; private set; } = default!;
        public string Slug { get; private set; } = default!;
        public string Excerpt { get; private set; } = default!;
        public string Status { get; private set; } = default!;
        public string FeaturedImage { get; private set; } = default!;
        public int ViewsCount { get; private set; } = default!;
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; } = default!;

        private Article() { }

        public Article(
            Guid id,
            string title,
            string content,
            string slug,
            string excerpt,
            string status,
            string featuredImage,
            int viewsCount,
            Guid categoryId)
        {
            Id = id;
            Title = title;
            Content = content;
            Slug = slug;
            Excerpt = excerpt;
            Status = status;
            FeaturedImage = featuredImage;
            ViewsCount = viewsCount;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            CategoryId = categoryId;
        }

    }
}
