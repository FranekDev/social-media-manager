namespace KanriSocial.Application.Features.Instagram.Post.Commands;

public record CreateInstagramPostRequest
{
    public string ImageUrl { get; set; }
    public string Caption { get; set; }
    public DateTime ScheduledAt { get; set; }
}