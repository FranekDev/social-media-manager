using KanriSocial.Application.Services.Instagram.Interfaces;
using KanriSocial.Infrastructure.Clients;
using KanriSocial.Infrastructure.Clients.Interfaces;

namespace KanriSocial.Application.Services.Instagram;

public class InstagramService(IInstagramClient instagramClient) : IInstagramService
{
    private readonly IInstagramClient _instagramClient = instagramClient;
    
    public async Task<string?> GetLongLivedToken(string accessToken)
    {
        var result = await _instagramClient.GetLongLivedToken(accessToken);
        if (result.IsFailed)
        {
            return null;
        }
     
        return result?.Value?.AccessToken;
    }
    
}