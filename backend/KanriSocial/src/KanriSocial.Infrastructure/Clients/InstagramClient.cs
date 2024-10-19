using FluentResults;
using KanriSocial.Domain.Dtos.Instagram;
using KanriSocial.Infrastructure.Clients.Interfaces;
using KanriSocial.Shared.Dtos.Instagram;
using Microsoft.Extensions.Configuration;

namespace KanriSocial.Infrastructure.Clients;

public class InstagramClient(IHttpClientFactory httpClientFactory, IConfiguration configuration) 
    : BaseHttpClient(httpClientFactory, nameof(InstagramClient)), IInstagramClient
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(nameof(InstagramClient));
    private readonly IConfiguration _configuration = configuration;
    
    public async Task<Result<InstagramTokenResponse?>> GetLongLivedToken(string accessToken)
    {
        var queryParams = new Dictionary<string, string?>
        {
            ["grant_type"] = "fb_exchange_token",
            ["client_id"] = _configuration["Instagram:ClientId"],
            ["client_secret"] = _configuration["Instagram:ClientSecret"],
            ["fb_exchange_token"] = accessToken
        };

        var uri = BuildUri("oauth/access_token", queryParams);
        return await GetFromApiWithFailMessage<InstagramTokenResponse>(uri, "Failed to get long-lived token");
    }

    public async Task<Result<InstagramContainer?>> GetMedia(string instagramUserId, string imageUrl, string? caption, string accessToken)
    {
        var queryParams = new Dictionary<string, string?>
        {
            ["image_url"] = imageUrl,
            ["caption"] = caption,
            ["access_token"] = accessToken
        };

        var uri = BuildUri($"{instagramUserId}/media", queryParams);
        return await PostToApiWithFailMessage<InstagramContainer>(uri, null, "Failed to get media");
    }

    public async Task<Result<InstagramMedia?>> PublishMedia(string instagramUserId, InstagramContainer container, string accessToken)
    {
        var queryParams = new Dictionary<string, string?>
        {
            ["creation_id"] = container.Id,
            ["access_token"] = accessToken
        };

        var uri = BuildUri($"{instagramUserId}/media_publish", queryParams);
        return await PostToApiWithFailMessage<InstagramMedia>(uri, null, "Failed to publish media");
    }

    public async Task<Result<InstagramUserDetail?>> GetUserDetail(string instagramUserId, string accessToken)
    {
        List<string> fields = ["username", "biography", "media_count", "followers_count", "follows_count", "name", "profile_picture_url", "website"];
        
        var queryParams = new Dictionary<string, string?>
        {
            ["fields"] = string.Join(",", fields),
            ["access_token"] = accessToken
        };
        
        var uri = BuildUri(instagramUserId, queryParams);
        return await GetFromApiWithFailMessage<InstagramUserDetail>(uri, "Failed to get user details");
    }
    
    public async Task<Result<InstagramMediaDetail?>> GetMediaDetail(string instagramMediaId, string accessToken)
    {
        List<string> fields = [
            "caption", 
            "comments_count", 
            "copyright_check_information", 
            "id", 
            "is_comment_enabled", 
            "is_shared_to_feed", 
            "like_count", 
            "media_product_type", 
            "media_type", 
            "media_url", 
            "owner", 
            "permalink", 
            "shortcode", 
            "thumbnail_url", 
            "timestamp", 
            "username"];
        // check required permissions
        var queryParams = new Dictionary<string, string?>
        {
            ["fields"] = string.Join(",", fields),
            ["access_token"] = accessToken
        };
        
        var uri = BuildUri(instagramMediaId, queryParams);
        return await GetFromApiWithFailMessage<InstagramMediaDetail>(uri, "Failed to get media detail");
    }
}