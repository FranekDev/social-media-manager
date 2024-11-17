using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaManager.Domain.Models.TikTok;

public class TikTokUser
{
    [Column(TypeName = "uuid")]
    public Guid Id { get; init; } = Guid.NewGuid();
    
    public string UserId { get; init; }
    public User User { get; init; }
    
    public string Token { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public IEnumerable<TikTokVideo> TikTokVideos { get; set; }
}