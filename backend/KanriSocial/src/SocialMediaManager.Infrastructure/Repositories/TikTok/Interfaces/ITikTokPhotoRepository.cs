using SocialMediaManager.Domain.Models.TikTok;

namespace SocialMediaManager.Infrastructure.Repositories.TikTok.Interfaces;

public interface ITikTokPhotoRepository
{
    Task<Guid> Create(TikTokPhoto video);
    Task<IEnumerable<TikTokPhoto>> GetAll(); 
    Task<TikTokPhoto?> GetById(Guid id);
    Task Update(TikTokPhoto video);
    Task Delete(Guid id);
    Task<IEnumerable<TikTokPhoto>> GetByTikTokUserId(Guid tikTokUserId);
    Task<IEnumerable<TikTokPhoto>> GetUnpublishedByUserId(Guid userId);
    Task<IEnumerable<TikTokPhoto>> GetPublishedByUserId(Guid userId);
}