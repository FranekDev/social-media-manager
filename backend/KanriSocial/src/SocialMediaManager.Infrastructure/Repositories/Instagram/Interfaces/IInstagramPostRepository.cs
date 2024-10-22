using SocialMediaManager.Domain.Models.Instagram.Post;
using SocialMediaManager.Domain.Models.Instagram.User;

namespace SocialMediaManager.Infrastructure.Repositories.Instagram.Interfaces;

public interface IInstagramPostRepository
{
    Task<Guid> Create(InstagramPost post);
    Task<IEnumerable<InstagramPost>> GetAll(); 
    Task<InstagramPost?> GetById(Guid id);
    Task Update(InstagramPost post);
    Task Delete(Guid id);
    Task<IEnumerable<InstagramUser>> GetByInstagramUserId(Guid instagramUserId);
    Task<IEnumerable<InstagramPost>> GetUnpublishedPosts(Guid userId);
}