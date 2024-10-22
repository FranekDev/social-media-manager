using SocialMediaManager.Domain.Models.Instagram.Post;
using SocialMediaManager.Domain.Models.Instagram.User;

namespace SocialMediaManager.Infrastructure.Repositories.Instagram.Interfaces;

public interface IInstagramReelRepository
{
    Task<Guid> Create(InstagramReel reel);
    Task<IEnumerable<InstagramReel>> GetAll(); 
    Task<InstagramReel?> GetById(Guid id);
    Task Update(InstagramReel reel);
    Task Delete(Guid id);
    Task<IEnumerable<InstagramUser>> GetByInstagramUserId(Guid instagramUserId);
    Task<IEnumerable<InstagramReel>> GetUnpublishedReels(Guid userId);
}