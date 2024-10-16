using KanriSocial.Domain.Dtos.Instagram;
using KanriSocial.Domain.Models.Instagram.Post;
using KanriSocial.Domain.Models.Instagram.User;

namespace KanriSocial.Infrastructure.Repositories.Instagram.Interfaces;

public interface IInstagramPostRepository
{
    Task<Guid> Create(InstagramPost post);
    Task<IEnumerable<InstagramPost>> GetAll(); 
    Task<InstagramPost?> GetById(Guid id);
    Task Update(InstagramPost post);
    Task Delete(Guid id);
    Task<IEnumerable<InstagramUser>> GetByInstagramUserId(Guid instagramUserId);
    void Detach(InstagramPost post);
}