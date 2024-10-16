using System.ComponentModel.DataAnnotations;

namespace KanriSocial.Domain.Dtos.Account;

public class RegisterDto
{
    [Required]
    public string? Username { get; set; }
    
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    
    [Required]
    public string? Password { get; set; }
}