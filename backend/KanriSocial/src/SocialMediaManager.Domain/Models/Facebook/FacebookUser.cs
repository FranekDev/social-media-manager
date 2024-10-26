using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaManager.Domain.Models.Facebook;

public class FacebookUser
{
    [Column(TypeName = "uuid")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string AccountId { get; init; }
    
    public string UserId { get; init; }
    
    public User User { get; init; }
    
    public string Token { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public IEnumerable<FacebookFeedPost> FacebookFeedPosts { get; set; }
}