using KanriSocial.Domain.Models;
using KanriSocial.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace KanriSocial.Infrastructure.Repositories;

public class UserTokenRepository(KanriSocialDbContext context)
{
    public async Task Create(UserToken userToken)
    {
        await context.UserTokens.AddAsync(userToken);
        await context.SaveChangesAsync();
    }
    
    public async Task<UserToken?> GetByUserId(string userId)
    {
        return await context.UserTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.UserId == userId);
    }
    
    public async Task<UserToken?> GetByToken(string token)
    {
        return await context.UserTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == token);
    }
    
    public async Task Delete(string userId)
    {
        var userToken = await context.UserTokens.FirstOrDefaultAsync(x => x.UserId == userId);
        if (userToken == null)
        {
            throw new Exception("User token not found");
        }
        
        context.UserTokens.Remove(userToken);
        await context.SaveChangesAsync();
    }
    
    public async Task Update(UserToken userToken)
    {
        context.UserTokens.Update(userToken);
        await context.SaveChangesAsync();
    }
}