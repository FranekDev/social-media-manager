using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaManager.Domain.Models.TikTok;

public class TikTokPhoto
{
    [Column(TypeName = "uuid")]
    public Guid Id { get; init; } = Guid.NewGuid();
    
    public Guid TikTokUserId { get; init; }
    public TikTokUser TikTokUser { get; init; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public List<string> PhotoImagesUrls { get; set; }
    
    public DateTime ScheduledAt { get; set; }

    public string? PublishId { get; set; } = null;

    public bool IsPublished { get; set; } = false;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}