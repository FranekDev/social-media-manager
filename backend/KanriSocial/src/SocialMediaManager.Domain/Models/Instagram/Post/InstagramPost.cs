using System.ComponentModel.DataAnnotations;
using SocialMediaManager.Domain.Models.Instagram.User;

namespace SocialMediaManager.Domain.Models.Instagram.Post;

public record InstagramPost
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();
    
    [Required]
    public Guid InstagramUserId { get; init; }
    public InstagramUser InstagramUser { get; init; }
    
    [Required]
    [Url]
    public string ImageUrl { get; set; }
    
    //public byte[] ImageBytes { get; set; }
    
    [Required]
    [StringLength(2200)]
    public string? Caption { get; set; }
    
    [Required]
    public DateTime ScheduledAt { get; set; }
    
    [Required]
    public string ContainerId { get; set; }

    public bool IsPublished { get; set; } = false;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
