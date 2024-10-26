using Microsoft.EntityFrameworkCore;
using SocialMediaManager.Domain.Models.Facebook;
using SocialMediaManager.Infrastructure.Database;
using SocialMediaManager.Infrastructure.Repositories.Facebook.Interfaces;

namespace SocialMediaManager.Infrastructure.Repositories.Facebook;

public class FacebookUserRepository(KanriSocialDbContext context) : IFacebookUserRepository
{
    private readonly KanriSocialDbContext _context = context;
    
    public async Task<Guid> Create(FacebookUser user)
    {
        var newUser = await _context.FacebookUsers.AddAsync(user);
        await _context.SaveChangesAsync();
        
        return newUser.Entity.Id;
    }

    public async Task<FacebookUser?> GetByAccountId(string accountId)
    {
        return await _context.FacebookUsers.FirstOrDefaultAsync(u => u.AccountId == accountId);
    }

    public async Task<FacebookUser?> GetByUserId(Guid userId)
    {
        return await _context.FacebookUsers.FirstOrDefaultAsync(u => u.UserId == userId.ToString());
    }
}