using LetterBox.Application.Accounts.DataModels;
using LetterBox.Application.Authorization;
using LetterBox.Infrastructure.Authentication.IdentityManagers;
using LetterBox.Infrastructure.Authentication.Options;
using LetterBox.Infrastructure.Authentication.Seeding;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LetterBox.Infrastructure.Authentication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureAthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<AccountsDbContext>();

            services.AddTransient<ITokenProvider, JwtTokenProvider>();

            services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();

            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

            services.RegisterIdentity();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var jwtOptions = configuration.GetSection(JwtOptions.JWT).Get<JwtOptions>()
                        ?? throw new ApplicationException("Missing jwt configurations");

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        //ClockSkew = TimeSpan.Zero
                    };
                });

            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.JWT));
            services.Configure<AdminOptions>(configuration.GetSection(AdminOptions.ADMIN));

            services.AddAuthorization(options =>
            {
                //options.DefaultPolicy = new AuthorizationPolicyBuilder()
                //    .RequireClaim("Role", "User")
                //    .RequireAuthenticatedUser()
                //    .Build();

                //options.AddPolicy("PermissionRequirement",
                //    policy => { policy.AddRequirements(new PermissionAttribute("create.category")); });

                //options.AddPolicy("PermissionRequirement",
                //    policy => { policy.AddRequirements(new PermissionAttribute("create.article")); });
            });

            services.AddSingleton<AccountsSeeder>(); 
            services.AddScoped<AccountsSeederService>(); 


            return services;
        }

        private static void RegisterIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<User, Role>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<AccountsDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<PermissionManager>();
            services.AddScoped<RolePermissionManager>();
            services.AddScoped<AdminAccountManager>();

        }
    }
}
