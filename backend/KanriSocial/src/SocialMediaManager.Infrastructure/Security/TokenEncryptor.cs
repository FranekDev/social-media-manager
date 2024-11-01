using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;

namespace SocialMediaManager.Infrastructure.Security;

public class TokenEncryptor(IDataProtectionProvider provider, IConfiguration configuration) : ITokenEncryptor
{
    private readonly IDataProtector _protector = provider.CreateProtector(configuration["TokenEncryptionKey"]);
    
    public string Encrypt(string token) => _protector.Protect(token);
    public string Decrypt(string token) => _protector.Unprotect(token);
}