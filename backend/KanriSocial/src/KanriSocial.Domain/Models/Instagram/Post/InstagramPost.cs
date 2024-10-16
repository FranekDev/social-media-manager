using System.ComponentModel.DataAnnotations;
using KanriSocial.Domain.Models.Instagram.User;

namespace KanriSocial.Domain.Models.Instagram.Post;

public record InstagramPost
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();
    
    [Required]
    public Guid InstagramUserId { get; init; }
    public InstagramUser InstagramUser { get; init; }
    
    [Required]
    [Url]
    public string ImageUrl { get; init; }
    
    [Required]
    [StringLength(2200)]
    public string? Caption { get; init; }
    
    [Required]
    public DateTime ScheduledAt { get; init; }
    
    [Required]
    public string ContainerId { get; init; }

    public bool IsPublished { get; init; } = false;
    
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}
