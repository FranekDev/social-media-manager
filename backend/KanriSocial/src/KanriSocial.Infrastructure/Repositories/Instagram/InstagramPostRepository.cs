using AutoMapper;
using KanriSocial.Domain.Dtos.Instagram;
using KanriSocial.Domain.Models.Instagram.Post;
using KanriSocial.Domain.Models.Instagram.User;
using KanriSocial.Infrastructure.Database;
using KanriSocial.Infrastructure.Repositories.Instagram.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KanriSocial.Infrastructure.Repositories.Instagram;

public class InstagramPostRepository(KanriSocialDbContext context, IMapper mapper) : IInstagramPostRepository
{
    private readonly KanriSocialDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    
    public async Task<Guid> Create(InstagramPost post)
    {
        var instagramPost = _mapper.Map<InstagramPost>(post);
        var newPost = await _context.InstagramPosts.AddAsync(instagramPost);
        await _context.SaveChangesAsync();
        return newPost.Entity.Id;
    }

    public async Task<IEnumerable<InstagramPost>> GetAll()
    {
        return await _context.InstagramPosts.ToListAsync();
    }

    public async Task<InstagramPost?> GetById(Guid id)
    {
        return await _context.InstagramPosts.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Update(InstagramPost post)
    {
        _context.InstagramPosts.Update(post);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var post = await _context.InstagramPosts.FirstOrDefaultAsync(x => x.Id == id);
        if (post == null)
        {
            throw new Exception("Post not found");
        }
        
        _context.InstagramPosts.Remove(post);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<InstagramUser>> GetByInstagramUserId(Guid instagramUserId)
    {
        return await _context.InstagramUsers.Where(x => x.Id == instagramUserId).ToListAsync();
        // return [];
    }
    
    public void Detach(InstagramPost post)
    {
        _context.Entry(post).State = EntityState.Detached;
    }
}