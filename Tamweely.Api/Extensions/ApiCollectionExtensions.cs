using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Tamweely.Api.Middlewares;
using Tamweely.Domain.Entities;
using Tamweely.Infrastructure.Data;
using Tamweely.Infrastructure.Extensions;
using Tamweely.Application.Extensions;

namespace Tamweely.Api.Extensions;

public static class ApiCollectionExtensions
{
    public static void AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddInfrastructure(configuration);
        services.AddApplicationServices();
        services.AddSwaggerGen(options => //code to add the JWT authentication in Swagger is constant for all projects
        {
            // Add JWT Bearer definition
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter 'Bearer' [space] and then your valid JWT token.\nExample: Bearer eyJhbGciOiJIUzI1NiIs..."
            });

            // Add requirement so Swagger shows the lock button
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new  OpenApiSecurityScheme
            {
                Reference = new  OpenApiReference
                {
                    Type =  ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
        });

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins("http://localhost:4200");
            });
        });

        services.AddScoped<ErrorHandlingMiddleware>();

        services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<TamweelyDbContext>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = false;
            o.SaveToken = false;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                ClockSkew = TimeSpan.Zero // Important: removes default 5 min delay
            };
        });
    }
}
