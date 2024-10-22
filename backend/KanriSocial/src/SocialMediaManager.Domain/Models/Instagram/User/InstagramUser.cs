using System.ComponentModel.DataAnnotations;
using SocialMediaManager.Domain.Models.Instagram.Post;

namespace SocialMediaManager.Domain.Models.Instagram.User;

public class InstagramUser
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();
    public string AccountId { get; init; }
    public string UserId { get; init; }
    public Models.User User { get; init; }
    public string Token { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<InstagramPost> InstagramPosts { get; init; } = new List<InstagramPost>();
}