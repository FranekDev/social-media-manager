using KanriSocial.Domain.Models;

namespace KanriSocial.Application.Services.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}