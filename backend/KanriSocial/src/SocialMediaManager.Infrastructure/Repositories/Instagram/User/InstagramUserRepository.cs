using SocialMediaManager.Domain.Models.Instagram.User;
using Microsoft.EntityFrameworkCore;
using SocialMediaManager.Infrastructure.Database;
using SocialMediaManager.Infrastructure.Repositories.Instagram.Interfaces.User;

namespace SocialMediaManager.Infrastructure.Repositories.Instagram.User;

public class InstagramUserRepository(KanriSocialDbContext context) : IInstagramUserRepository
{
    private readonly KanriSocialDbContext _context = context;
    
    public async Task<Guid> Create(InstagramUser user)
    {
        var instagramUser = _context.InstagramUsers.Add(user);
        await _context.SaveChangesAsync();
        
        return instagramUser.Entity.Id;
    }
 
    public async Task<IEnumerable<InstagramUser>> GetAll()
    {
        return await _context.InstagramUsers.ToListAsync();
    }
    
    public async Task<InstagramUser?> GetByUserId(Guid userId)
    {
        return await _context.InstagramUsers.FirstOrDefaultAsync(x => x.UserId == userId.ToString());
    }
    
    public async Task<InstagramUser?> GetByInstagramUserId(Guid instagramUserId)
    {
        return await _context.InstagramUsers.FirstOrDefaultAsync(x => x.Id == instagramUserId);
    }
}