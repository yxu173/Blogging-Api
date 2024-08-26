using Domain.Entities;
using Infrastracture.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastracture;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options
            => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        return services;
    }
}