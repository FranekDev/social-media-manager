using Microsoft.EntityFrameworkCore;
using SocialMediaManager.Domain.Models.TikTok;
using SocialMediaManager.Infrastructure.Database;
using SocialMediaManager.Infrastructure.Repositories.TikTok.Interfaces;

namespace SocialMediaManager.Infrastructure.Repositories.TikTok;

public class TikTokVideoRepository(KanriSocialDbContext context) : ITikTokVideoRepository
{
    private readonly KanriSocialDbContext _context = context;
    
    public async Task<Guid> Create(TikTokVideo video)
    {
        var newVideo = await _context.TikTokVideos.AddAsync(video);
        await _context.SaveChangesAsync();
        return newVideo.Entity.Id;
    }

    public async Task<IEnumerable<TikTokVideo>> GetAll()
    {
        return await _context.TikTokVideos.ToListAsync();
    }

    public async Task<TikTokVideo?> GetById(Guid id)
    {
        return await _context.TikTokVideos.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Update(TikTokVideo video)
    {
        _context.TikTokVideos.Update(video);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var video = await _context.TikTokVideos.FirstOrDefaultAsync(x => x.Id == id);
        if (video != null)
        {
            _context.TikTokVideos.Remove(video);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<TikTokVideo>> GetByTikTokUserId(Guid tikTokUserId)
    {
        return await _context.TikTokVideos
            .Where(x => x.TikTokUserId == tikTokUserId)
            .ToListAsync();
    }

    public async Task<IEnumerable<TikTokVideo>> GetUnpublished(Guid userId)
    {
        return await _context.TikTokVideos
            .Where(x => x.TikTokUser.UserId == userId.ToString() && !x.IsPublished)
            .ToListAsync();             
    }
}