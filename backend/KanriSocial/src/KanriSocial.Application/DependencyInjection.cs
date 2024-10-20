using System.Reflection;
using System.Text;
using KanriSocial.Application.Behaviors;
using KanriSocial.Application.Services;
using KanriSocial.Application.Services.Instagram;
using KanriSocial.Application.Services.Instagram.Interfaces;
using KanriSocial.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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

        // services.ConfigureAuthentication(configuration);
        
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
    
    // private static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    // {
    //     services.AddAuthentication(options =>
    //     {
    //         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //         options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
    //         options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    //         options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    //         options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
    //     }).AddJwtBearer(options =>
    //     {
    //         options.TokenValidationParameters = new TokenValidationParameters
    //         {
    //             ValidateIssuer = true,
    //             ValidIssuer = configuration["JWT:Issuer"],
    //             ValidateAudience = true,
    //             ValidAudience = configuration["JWT:Audience"],
    //             ValidateIssuerSigningKey = true,
    //             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SigningKey"])),
    //         };
    //     });
    //     return services;
    // }
}