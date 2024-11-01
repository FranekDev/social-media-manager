using Microsoft.EntityFrameworkCore;
using SocialMediaManager.Domain.Models.TikTok;
using SocialMediaManager.Infrastructure.Database;
using SocialMediaManager.Infrastructure.Repositories.TikTok.Interfaces;

namespace SocialMediaManager.Infrastructure.Repositories.TikTok;

public class TikTokUserRepository(KanriSocialDbContext context) : ITikTokUserRepository
{
    private readonly KanriSocialDbContext _context = context;
    
    public async Task<Guid> Create(TikTokUser user)
    {
        await _context.TikTokUsers.AddAsync(user);
        await _context.SaveChangesAsync();
        
        return user.Id;
    }

    public Task<TikTokUser?> GetByUserId(string userId)
    {
        return _context.TikTokUsers.FirstOrDefaultAsync(x => x.UserId == userId);
    }
}