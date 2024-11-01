using SocialMediaManager.Domain.Models.TikTok;

namespace SocialMediaManager.Infrastructure.Repositories.TikTok.Interfaces;

public interface ITikTokUserRepository
{
    Task<Guid> Create(TikTokUser user);
    Task<TikTokUser?> GetByUserId(string userId);
}