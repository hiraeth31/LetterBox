using LetterBox.Application.Accounts.DataModels;
using LetterBox.Application.Authorization;
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
            services.AddScoped<AuthorizationDbContext>();

            services.AddTransient<ITokenProvider, JwtTokenProvider>();

            services
                .AddIdentity<User, Role>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<AuthorizationDbContext>()
                .AddDefaultTokenProviders();

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

            services.Configure<JwtOptions>(
                configuration.GetSection(JwtOptions.JWT));

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

            return services;
        }
    }
}
