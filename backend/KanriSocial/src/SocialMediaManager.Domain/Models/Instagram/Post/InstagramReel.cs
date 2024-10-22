using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SocialMediaManager.Domain.Models.Instagram.User;

namespace SocialMediaManager.Domain.Models.Instagram.Post;

public class InstagramReel
{
    [Column(TypeName = "uuid")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    public string VideoUrl { get; set; }
    
    [MaxLength(2200)]
    public string? Caption { get; set; } = null;
    
    [Required]
    public DateTime ScheduledAt { get; set; }
    
    public Guid InstagramUserId { get; set; }
    
    public InstagramUser InstagramUser { get; set; }
    
    public bool IsPublished { get; set; } = false;
    
    public string? ContainerId { get; set; } = null;
    
    public string? MediaId { get; set; } = null;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}