using System.Reflection;
using FluentValidation;
using KanriSocial.Application.Behaviors;
using KanriSocial.Application.Services;
using KanriSocial.Application.Services.Instagram;
using KanriSocial.Application.Services.Instagram.Interfaces;
using KanriSocial.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace KanriSocial.Application;

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
        services.AddScoped<IInstagramPostService, InstagramPostService>();
        services.AddScoped<IInstagramUserService, InstagramUserService>();
        return services;
    }
}