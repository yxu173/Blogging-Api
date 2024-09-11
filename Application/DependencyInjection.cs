using System.Reflection;
using Application.Options;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<UserServices>();
        services.AddScoped<UploadPhotoServices>();
        services.AddScoped<JwtSettings>();
        services.AddScoped<PostServices>();
        var jwtSettings = new JwtSettings();
        config.Bind(nameof(JwtSettings), jwtSettings);
        var jwtSection = config.GetSection(nameof(JwtSettings));
        services.Configure<JwtSettings>(jwtSection);

        services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding
                        .UTF8.GetBytes(jwtSettings.SigningKey))
                };
            });
        services.AddAuthorization();
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}