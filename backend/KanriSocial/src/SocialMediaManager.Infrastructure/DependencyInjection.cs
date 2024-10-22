using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaManager.Infrastructure.Clients;
using SocialMediaManager.Infrastructure.Clients.Interfaces;
using SocialMediaManager.Infrastructure.Database;
using SocialMediaManager.Infrastructure.Repositories;
using SocialMediaManager.Infrastructure.Repositories.Instagram;
using SocialMediaManager.Infrastructure.Repositories.Instagram.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.Instagram.Interfaces.User;
using SocialMediaManager.Infrastructure.Repositories.Instagram.User;

namespace SocialMediaManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<KanriSocialDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("KanriSocialDatabase"))
        );

        services.AddRepositories();
        services.AddClients(configuration);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IInstagramUserRepository, InstagramUserRepository>();
        services.AddScoped<IInstagramPostRepository, InstagramPostRepository>();
        services.AddScoped<UserTokenRepository>();
        
        return services;
    }
    
    private static IServiceCollection AddClients(this IServiceCollection services, IConfiguration configuration)
    {        
        services.AddHttpClient<InstagramClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["Instagram:BaseUrl"]);
        });

        services.AddTransient<IInstagramClient, InstagramClient>();
        
        return services;
    }
}