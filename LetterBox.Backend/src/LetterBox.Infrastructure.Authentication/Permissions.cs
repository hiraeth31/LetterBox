namespace LetterBox.Infrastructure.Authentication
{
    public static class Permissions
    {
        public static class Articles
        {
            public const string Create = "articles.create";
            public const string Update = "articles.update";
            public const string Delete = "articles.delete";
            public const string Read = "articles.read";
        }

        public static class Categories
        {
            public const string Create = "categories.create";
            public const string Update = "categories.update";
            public const string Delete = "categories.delete";
            public const string Read = "categories.read";
        }
    }
}
