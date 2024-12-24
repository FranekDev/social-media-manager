using Microsoft.EntityFrameworkCore;
using SocialMediaManager.Domain.Models.TikTok;
using SocialMediaManager.Infrastructure.Database;
using SocialMediaManager.Infrastructure.Repositories.TikTok.Interfaces;

namespace SocialMediaManager.Infrastructure.Repositories.TikTok;

public class TikTokPhotoRepository(KanriSocialDbContext context) : ITikTokPhotoRepository
{
    private readonly KanriSocialDbContext _context = context;
    
    public async Task<Guid> Create(TikTokPhoto video)
    {
        var newVideo = await _context.TikTokPhotos.AddAsync(video);
        await _context.SaveChangesAsync();
        return newVideo.Entity.Id;
    }

    public async Task<IEnumerable<TikTokPhoto>> GetAll()
    {
        return await _context.TikTokPhotos.ToListAsync();
    }

    public async Task<TikTokPhoto?> GetById(Guid id)
    {
        return await _context.TikTokPhotos.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Update(TikTokPhoto video)
    {
        _context.TikTokPhotos.Update(video);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var video = await _context.TikTokPhotos.FirstOrDefaultAsync(x => x.Id == id);
        if (video != null)
        {
            _context.TikTokPhotos.Remove(video);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<TikTokPhoto>> GetByTikTokUserId(Guid tikTokUserId)
    {
        return await _context.TikTokPhotos
            .Where(x => x.TikTokUserId == tikTokUserId)
            .ToListAsync();
    }

    public async Task<IEnumerable<TikTokPhoto>> GetUnpublishedByUserId(Guid userId)
    {
        return await _context.TikTokPhotos
            .Where(x => x.TikTokUser.UserId == userId.ToString() && !x.IsPublished)
            .ToListAsync();             
    }
    
    public async Task<IEnumerable<TikTokPhoto>> GetPublishedByUserId(Guid userId)
    {
        return await _context.TikTokPhotos
            .Where(x => x.TikTokUser.UserId == userId.ToString() && x.IsPublished)
            .ToListAsync();             
    }
}