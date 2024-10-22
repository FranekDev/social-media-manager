using SocialMediaManager.Domain.Models;

namespace SocialMediaManager.Application.Services.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(User user);
    Task<string?> ValidateToken(string token);
}