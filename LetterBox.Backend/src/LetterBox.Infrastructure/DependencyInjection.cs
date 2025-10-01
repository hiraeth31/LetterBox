using LetterBox.Application.Articles;
using LetterBox.Application.Categories;
using LetterBox.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LetterBox.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IArticlesRepository, ArticlesRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();

            return services;
        }
    }
}
