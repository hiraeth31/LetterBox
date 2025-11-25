using FluentValidation;
using LetterBox.Application.Accounts.LoginUser;
using LetterBox.Application.Accounts.RefreshTokens;
using LetterBox.Application.Accounts.RegisterUser;
using LetterBox.Application.Articles.AddArticle;
using LetterBox.Application.Articles.Queries;
using LetterBox.Application.Categories.AddCategory;
using Microsoft.Extensions.DependencyInjection;

namespace LetterBox.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<AddArticleHandler>();
            services.AddScoped<GetArticleByIdHandler>();

            services.AddScoped<AddCategoryHandler>();
            services.AddScoped<RegisterUserHandler>();
            services.AddScoped<RefreshTokensHandler>();
            services.AddScoped<LoginHandler>();

            services.AddScoped<GetArticleHandler>();

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly); // добавление всех валидаторов со сборки

            return services;
        }
    }
}
