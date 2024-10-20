using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KanriSocial.Domain.Models;

public record UserToken
{
    [Required]
    [Column(TypeName = "uuid")]
    public Guid Id { get; init; }
    
    [Required]
    public string UserId { get; init; }
    public User User { get; init; }
    
    [Required]
    public string Token { get; set; }
    
    [Required]
    public bool IsValid { get; set; }
    
    [Required]
    public DateTime ExpiresAt { get; set; }
}