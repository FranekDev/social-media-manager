using Microsoft.EntityFrameworkCore;
using SocialMediaManager.Domain.Models.Facebook;
using SocialMediaManager.Infrastructure.Database;
using SocialMediaManager.Infrastructure.Repositories.Facebook.Interfaces;

namespace SocialMediaManager.Infrastructure.Repositories.Facebook;

public class FacebookFeedRepository(KanriSocialDbContext context) : IFacebookFeedRepository
{
    private readonly KanriSocialDbContext _context = context;
    
    public async Task<Guid> Create(FacebookFeedPost feedPost)
    {
        var newPost = await _context.FacebookFeedPosts.AddAsync(feedPost);
        await _context.SaveChangesAsync();
        return newPost.Entity.Id;
    }

    public async Task Update(FacebookFeedPost feedPost)
    {
        _context.FacebookFeedPosts.Update(feedPost);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<FacebookFeedPost>> GetUnpublishedPosts(Guid userId)
    {
        return await _context.FacebookFeedPosts
            .Where(x => x.FacebookUser.UserId == userId.ToString() && !x.IsPublished)
            .ToListAsync();
    }

    public async Task<FacebookFeedPost?> GetById(Guid id)
    {
        return await _context.FacebookFeedPosts.FirstOrDefaultAsync(x => x.Id == id);
    }
}