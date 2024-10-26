namespace SocialMediaManager.Shared.Dtos.Facebook;

public record FacebookPageData(
    string Id,
    string About,
    string Category,
    List<Category> CategoryList,
    int Checkins,
    ConnectedInstagramAccount ConnectedInstagramAccount,
    int FollowersCount,
    bool IsPublished,
    string Link,
    string Name,
    int RatingCount
);