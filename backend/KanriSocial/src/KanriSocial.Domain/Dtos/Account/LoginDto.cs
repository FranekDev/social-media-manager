using System.ComponentModel.DataAnnotations;

namespace KanriSocial.Domain.Dtos.Account;

public class LoginDto
{
    [Required]
    public string Username { get; set; }
    
    [Required]
    public string Password { get; set; }
}