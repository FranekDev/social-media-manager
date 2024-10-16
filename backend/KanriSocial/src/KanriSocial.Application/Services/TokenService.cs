using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KanriSocial.Application.Services.Interfaces;
using KanriSocial.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace KanriSocial.Application.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly SymmetricSecurityKey _key = new(Encoding.UTF8.GetBytes(configuration["JWT:SigningKey"]));
    
    public string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.GivenName, user.UserName),
            new(ClaimTypes.NameIdentifier, user.Id)
        };
        
        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = credentials,
            Issuer = _configuration["JWT:Issuer"],
            Audience = _configuration["JWT:Audience"]
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }
}