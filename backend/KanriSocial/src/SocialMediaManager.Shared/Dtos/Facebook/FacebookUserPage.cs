namespace SocialMediaManager.Shared.Dtos.Facebook;

public record FacebookUserPage
{
    public string Category { get; init; }
    public List<Category> CategoryList { get; init; }
    public string Name { get; init; }
    public string PageId { get; init; }
    public List<string> Tasks { get; init; }
    public FacebookPagePicture? PagePicture { get; init; } = null;
};