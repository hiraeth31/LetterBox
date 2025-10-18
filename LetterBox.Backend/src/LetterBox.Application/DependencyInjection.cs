using LetterBox.Application.Accounts.LoginUser;
using LetterBox.Application.Accounts.RegisterUser;
using LetterBox.Application.Articles.AddArticle;
using LetterBox.Application.Categories.AddCategory;
using Microsoft.Extensions.DependencyInjection;

namespace LetterBox.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<AddArticleHandler>();
            services.AddScoped<AddCategoryHandler>();
            services.AddScoped<RegisterUserHandler>();
            services.AddScoped<LoginUserHandler>();

            return services;
        }
    }
}
