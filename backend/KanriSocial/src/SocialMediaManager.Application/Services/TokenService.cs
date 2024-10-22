using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SocialMediaManager.Domain.Models;
using SocialMediaManager.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMediaManager.Application.Services.Interfaces;

namespace SocialMediaManager.Application.Services;

public class TokenService(IConfiguration configuration, UserTokenRepository userTokenRepository) : ITokenService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly UserTokenRepository _userTokenRepository = userTokenRepository;
    private readonly SymmetricSecurityKey _key = new(Encoding.UTF8.GetBytes(configuration["JWT:SigningKey"]));
    
    public async Task<string> CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.GivenName, user.UserName),
            new(ClaimTypes.NameIdentifier, user.Id)
        };
        
        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        
        var expiresAt = DateTime.UtcNow.AddDays(7);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiresAt,
            SigningCredentials = credentials,
            Issuer = _configuration["JWT:Issuer"],
            Audience = _configuration["JWT:Audience"]
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        
        var userToken = new UserToken
        {
            Token = tokenString,
            UserId = user.Id,
            ExpiresAt = expiresAt,
            IsValid = true
        };
        
        await _userTokenRepository.Create(userToken);
        
        return tokenString;
    }

    public async Task<string?> ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]);
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = _configuration["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = _configuration["JWT:Audience"],
            ValidateLifetime = true
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
            var userId = userIdClaim?.Value;

            if (userId is null)
            {
                return null;
            }

            var userToken = await _userTokenRepository.GetByUserId(userId);
            if (userToken is null || userToken.Token != token || !userToken.IsValid)
            {
                return null;
            }

            return userId;
        }
        catch
        {
            return null;
        }
    }
}