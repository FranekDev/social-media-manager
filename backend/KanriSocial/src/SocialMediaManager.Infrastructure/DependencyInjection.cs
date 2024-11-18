using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaManager.Infrastructure.Clients;
using SocialMediaManager.Infrastructure.Clients.Interfaces;
using SocialMediaManager.Infrastructure.Database;
using SocialMediaManager.Infrastructure.Repositories;
using SocialMediaManager.Infrastructure.Repositories.Facebook;
using SocialMediaManager.Infrastructure.Repositories.Facebook.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.Instagram;
using SocialMediaManager.Infrastructure.Repositories.Instagram.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.Instagram.Interfaces.User;
using SocialMediaManager.Infrastructure.Repositories.Instagram.User;
using SocialMediaManager.Infrastructure.Repositories.TikTok;
using SocialMediaManager.Infrastructure.Repositories.TikTok.Interfaces;
using SocialMediaManager.Infrastructure.Security;

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

        services.AddScoped<ITokenEncryptor, TokenEncryptor>();
        
        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IInstagramUserRepository, InstagramUserRepository>();
        services.AddScoped<IInstagramPostRepository, InstagramPostRepository>();
        services.AddScoped<UserTokenRepository>();
        services.AddScoped<IInstagramReelRepository, InstagramReelRepository>();
        services.AddScoped<IFacebookUserRepository, FacebookUserRepository>();
        services.AddScoped<IFacebookFeedRepository, FacebookFeedRepository>();
        services.AddScoped<ITikTokUserRepository, TikTokUserRepository>();
        services.AddScoped<ITikTokVideoRepository, TikTokVideoRepository>();
        services.AddScoped<ITikTokPhotoRepository, TikTokPhotoRepository>();
        
        return services;
    }
    
    private static IServiceCollection AddClients(this IServiceCollection services, IConfiguration configuration)
    {        
        services.AddHttpClient<InstagramClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["Instagram:BaseUrl"]);
        });

        services.AddHttpClient<FacebookClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["Instagram:BaseUrl"]);
        });
        
        services.AddHttpClient<TikTokClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["TikTok:BaseUrl"]);
        });

        services.AddTransient<IInstagramClient, InstagramClient>();
        services.AddTransient<IFacebookClient, FacebookClient>();
        services.AddTransient<ITikTokClient, TikTokClient>();
        
        return services;
    }
}