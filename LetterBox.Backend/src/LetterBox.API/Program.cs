using LetterBox.API.Middlewares;
using LetterBox.Application;
using LetterBox.Infrastructure;
using LetterBox.Infrastructure.Authentication;
using LetterBox.Infrastructure.Authentication.Seeding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
     new OpenApiSecurityScheme
     {
       Reference = new OpenApiReference
       {
         Type = ReferenceType.SecurityScheme,
         Id = "Bearer"
       }
      },
      new string[] { }
    }
  });
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services
    .AddInfrastructure()
    .AddApplication()
    .AddInfrastructureAthentication(builder.Configuration);

var app = builder.Build();

var accountsSeeder = app.Services.GetRequiredService<AccountsSeeder>();
await accountsSeeder.SeedAsync();

app.UseExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// добавление портов для работы с двух устройств
string _7028 = "https://0.0.0.0:7028";
string _5028 = "https://0.0.0.0:5028";

builder.WebHost.UseUrls(_7028, _5028);

app.UseCors(config =>
{
    string localHost = "http://localhost:3000";

    string ip_front = "niggers ahahaha";
    string port = ":3000";

    string httpsHost_front = "https://" + ip_front + port;
    string httpHost_front = "http://" + ip_front + port;

    // config.AllowAnyOrigin(); // на случай, если разрешение нужно дать всем

    config.WithOrigins(
        localHost,
        httpsHost_front,
        httpHost_front
    )
        .AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/api/categories", () =>
{
    string[] arr = ["category_1", "category_2"];
    return Results.Ok(arr);
});

app.Run();
