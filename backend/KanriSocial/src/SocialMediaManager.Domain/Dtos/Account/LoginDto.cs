using System.ComponentModel.DataAnnotations;

namespace SocialMediaManager.Domain.Dtos.Account;

public class LoginDto
{
    [Required]
    public string Username { get; set; }
    
    [Required]
    public string Password { get; set; }
}