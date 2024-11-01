namespace SocialMediaManager.Infrastructure.Security;

public interface ITokenEncryptor
{
    string Encrypt(string token);
    string Decrypt(string token);
}