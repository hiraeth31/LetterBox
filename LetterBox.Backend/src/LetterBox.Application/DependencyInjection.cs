using LetterBox.Application.Articles.AddArticle;
using Microsoft.Extensions.DependencyInjection;

namespace LetterBox.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<AddArticleHandler>();

            return services;
        }
    }
}
