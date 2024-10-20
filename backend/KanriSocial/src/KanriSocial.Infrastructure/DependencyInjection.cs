using System.Reflection;
using KanriSocial.Infrastructure.Clients;
using KanriSocial.Infrastructure.Clients.Builders;
using KanriSocial.Infrastructure.Clients.Interfaces;
using KanriSocial.Infrastructure.Database;
using KanriSocial.Infrastructure.Repositories;
using KanriSocial.Infrastructure.Repositories.Instagram;
using KanriSocial.Infrastructure.Repositories.Instagram.Interfaces;
using KanriSocial.Infrastructure.Repositories.Instagram.Interfaces.User;
using KanriSocial.Infrastructure.Repositories.Instagram.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KanriSocial.Infrastructure;

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

        // services.AddSingleton<InstagramClientBuilder>();
        // services.AddTransient<IInstagramClient>(provider =>
        // {
            // var builder = provider.GetRequiredService<InstagramClientBuilder>();
            // return builder.Build();
        // });
        
        return services;
    }
}