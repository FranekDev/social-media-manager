using SocialMediaManager.Domain.Models.TikTok;
using SocialMediaManager.Shared.Dtos.TikTok;
using TikTokVideo = SocialMediaManager.Domain.Models.TikTok.TikTokVideo;

namespace SocialMediaManager.Infrastructure.Repositories.TikTok.Interfaces;

public interface ITikTokVideoRepository
{
    Task<Guid> Create(TikTokVideo video);
    Task<IEnumerable<TikTokVideo>> GetAll(); 
    Task<TikTokVideo?> GetById(Guid id);
    Task Update(TikTokVideo video);
    Task Delete(Guid id);
    Task<IEnumerable<TikTokVideo>> GetByTikTokUserId(Guid tikTokUserId);
    Task<IEnumerable<TikTokVideo>> GetUnpublishedByUserId(Guid userId);
    Task<IEnumerable<TikTokVideo>> GetPublishedByUserId(Guid userId);
}