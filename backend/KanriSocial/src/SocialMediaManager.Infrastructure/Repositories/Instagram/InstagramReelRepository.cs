using AutoMapper;
using SocialMediaManager.Domain.Models.Instagram.Post;
using SocialMediaManager.Domain.Models.Instagram.User;
using Microsoft.EntityFrameworkCore;
using SocialMediaManager.Infrastructure.Database;
using SocialMediaManager.Infrastructure.Repositories.Instagram.Interfaces;

namespace SocialMediaManager.Infrastructure.Repositories.Instagram;

public class InstagramReelRepository(KanriSocialDbContext context, IMapper mapper) : IInstagramReelRepository
{
    private readonly KanriSocialDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Guid> Create(InstagramReel reel)
    {
        var newReel = await _context.InstagramReels.AddAsync(reel);
        await _context.SaveChangesAsync();
        return newReel.Entity.Id;
    }

    public async Task<IEnumerable<InstagramReel>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<InstagramReel?> GetById(Guid id)
    {
        return await _context.InstagramReels.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Update(InstagramReel reel)
    {
        _context.InstagramReels.Update(reel);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<InstagramUser>> GetByInstagramUserId(Guid instagramUserId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<InstagramReel>> GetUnpublishedReels(Guid userId)
    {
        return await _context.InstagramReels
            .Where(x => x.InstagramUser.UserId == userId.ToString() && !x.IsPublished)
            .ToListAsync();
    }
}