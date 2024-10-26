using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaManager.Domain.Models.Facebook;

public class FacebookFeedPost
{
    [Column(TypeName = "uuid")]
    public Guid Id { get; set; }
    
    [Required]
    public string PageId { get; set; }
    
    [Required]
    [MaxLength(63206)]
    public string Message { get; set; }
    
    [Required]
    public DateTime ScheduledAt { get; set; }
    
    [Required]
    [Column(TypeName = "uuid")]
    public Guid FacebookUserId { get; set; }
    public FacebookUser FacebookUser { get; set; }

    public string? PagegPostId { get; set; } = null;

    public bool IsPublished { get; set; } = false;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}