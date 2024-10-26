using SocialMediaManager.Domain.Models.Facebook;

namespace SocialMediaManager.Infrastructure.Repositories.Facebook.Interfaces;

public interface IFacebookFeedRepository
{
    Task<Guid> Create(FacebookFeedPost feedPost);
    Task Update(FacebookFeedPost feedPost);
    Task<IEnumerable<FacebookFeedPost>> GetUnpublishedPosts(Guid userId);
    Task<FacebookFeedPost?> GetById(Guid id);
}