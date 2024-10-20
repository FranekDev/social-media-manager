using KanriSocial.Domain.Models;

namespace KanriSocial.Application.Services.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(User user);
    Task<string?> ValidateToken(string token);
}