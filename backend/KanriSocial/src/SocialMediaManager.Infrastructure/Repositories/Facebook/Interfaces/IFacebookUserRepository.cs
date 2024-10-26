using SocialMediaManager.Domain.Models.Facebook;

namespace SocialMediaManager.Infrastructure.Repositories.Facebook.Interfaces;

public interface IFacebookUserRepository
{
    Task<Guid> Create(FacebookUser user);
    Task<FacebookUser?> GetByAccountId(string accountId);
    Task<FacebookUser?> GetByUserId(Guid userId);
}