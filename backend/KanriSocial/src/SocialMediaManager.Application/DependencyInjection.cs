using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaManager.Application.Behaviors;
using SocialMediaManager.Application.Services;
using SocialMediaManager.Application.Services.Facebook;
using SocialMediaManager.Application.Services.Facebook.Interfaces;
using SocialMediaManager.Application.Services.Instagram;
using SocialMediaManager.Application.Services.Instagram.Interfaces;
using SocialMediaManager.Application.Services.Interfaces;
using SocialMediaManager.Application.Services.TikTok;
using SocialMediaManager.Application.Services.TikTok.Interfaces;

namespace SocialMediaManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddServices();

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        return services;
    }
    
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IInstagramService, InstagramService>();
        services.AddScoped<IInstagramMediaService, InstagramMediaService>();
        services.AddScoped<IInstagramUserService, InstagramUserService>();
        services.AddScoped<IContentStorageService, ContentStorageService>();
        services.AddScoped<IFacebookService, FacebookService>();
        services.AddScoped<ITikTokVideoService, TikTokVideoService>();
        
        return services;
    }
}