namespace LetterBox.Domain.ArticlesManagement
{
    public sealed class Category
    {
        private Category() // ef core
        {
        }
        private readonly List<Article> _articles = [];
        public Guid Id { get; private set; }
        public string Name { get; private set; } = default!;
        public string Slug { get; private set; } = default!;
        public string Excerpt { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public DateTime CreatedAt { get; private set; }
        public IReadOnlyList<Article> Articles => _articles;
    }
}
