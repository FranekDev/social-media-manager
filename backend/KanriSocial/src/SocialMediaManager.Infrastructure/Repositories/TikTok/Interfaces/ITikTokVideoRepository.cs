using SocialMediaManager.Domain.Models.TikTok;

namespace SocialMediaManager.Infrastructure.Repositories.TikTok.Interfaces;

public interface ITikTokVideoRepository
{
    Task<Guid> Create(TikTokVideo video);
    Task<IEnumerable<TikTokVideo>> GetAll(); 
    Task<TikTokVideo?> GetById(Guid id);
    Task Update(TikTokVideo video);
    Task Delete(Guid id);
    Task<IEnumerable<TikTokVideo>> GetByTikTokUserId(Guid tikTokUserId);
    Task<IEnumerable<TikTokVideo>> GetUnpublished(Guid userId);
}