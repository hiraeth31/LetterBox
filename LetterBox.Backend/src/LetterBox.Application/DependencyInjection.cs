using FluentValidation;
using LetterBox.Application.Accounts.LoginUser;
using LetterBox.Application.Accounts.RefreshTokens;
using LetterBox.Application.Accounts.RegisterUser;
using LetterBox.Application.Articles.AddArticle;
using LetterBox.Application.Articles.Queries;
using LetterBox.Application.Articles.GetArticle;
using LetterBox.Application.Categories.AddCategory;
using Microsoft.Extensions.DependencyInjection;
using LetterBox.Application.Categories.GetCategory;

namespace LetterBox.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            #region articles
            services.AddScoped<AddArticleHandler>();
            services.AddScoped<GetArticleHandler>();
            services.AddScoped<GetArticleByIdHandler>();
            services.AddScoped<GetArticlesCountHandler>();
            #endregion

            #region categories
            services.AddScoped<AddCategoryHandler>();
            services.AddScoped<GetTotalCountCategoriesHandler>();
            services.AddScoped<GetTotalDataCategoriesHandler>();
            #endregion

            #region user auth
            services.AddScoped<RegisterUserHandler>();
            services.AddScoped<RefreshTokensHandler>();
            services.AddScoped<LoginHandler>();
            #endregion

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly); // добавление всех валидаторов со сборки

            return services;
        }
    }
}
