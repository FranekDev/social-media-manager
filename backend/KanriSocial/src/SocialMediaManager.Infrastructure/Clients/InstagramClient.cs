using FluentResults;
using SocialMediaManager.Shared.Dtos.Instagram;
using Microsoft.Extensions.Configuration;
using SocialMediaManager.Infrastructure.Clients.Interfaces;
using SocialMediaManager.Shared.Enums.Instagram;

namespace SocialMediaManager.Infrastructure.Clients;

public class InstagramClient(IHttpClientFactory httpClientFactory, IConfiguration configuration) 
    : BaseHttpClient(httpClientFactory, nameof(InstagramClient)), IInstagramClient
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(nameof(InstagramClient));
    private readonly IConfiguration _configuration = configuration;
    
    // add validation to refresh access token if publish date is more than 60 days
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
        List<string> fields = ["username", "biography", "media_count", "followers_count", "follows_count", "name", "profile_picture_url", "website", "media"];
        
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
        
        var queryParams = new Dictionary<string, string?>
        {
            ["fields"] = string.Join(",", fields),
            ["access_token"] = accessToken
        };
        
        var uri = BuildUri(instagramMediaId, queryParams);
        return await GetFromApiWithFailMessage<InstagramMediaDetail>(uri, "Failed to get media detail");
    }
    
    public async Task<Result<InstagramCommentData?>> GetMediaComments(string instagramMediaId, string accessToken)
    {
        var queryParams = new Dictionary<string, string?>
        {
            ["access_token"] = accessToken
        };
        
        var uri = BuildUri($"{instagramMediaId}/comments", queryParams);
        return await GetFromApiWithFailMessage<InstagramCommentData>(uri, "Failed to get media comments");
    }
    
    public async Task<Result<InstagramCommentData?>> GetCommentReplies(string commentId, string accessToken)
    {
        var queryParams = new Dictionary<string, string?>
        {
            ["access_token"] = accessToken
        };
        
        var uri = BuildUri($"{commentId}/replies", queryParams);
        return await GetFromApiWithFailMessage<InstagramCommentData>(uri, "Failed to get comment replies");
    }

    public Task<Result<InstagramCommentReply?>> ReplyToComment(string commentId, string accessToken, string message)
    {
        var queryParams = new Dictionary<string, string?>
        {
            ["message"] = message,
            ["access_token"] = accessToken
        };
        
        var uri = BuildUri($"{commentId}/replies", queryParams);
        return PostToApiWithFailMessage<InstagramCommentReply>(uri, null, "Failed to reply to comment");
    }

    public async Task<Result<InstagramContainer?>> GetReelMedia(string instagramUserId, string videoUrl, string? caption, string accessToken)
    {
        var queryParams = new Dictionary<string, string?>
        {
            ["video_url"] = videoUrl,
            ["caption"] = caption,
            ["media_type"] = "REELS",
            ["access_token"] = accessToken
        };
        
        var uri = BuildUri($"{instagramUserId}/media", queryParams);
        return await PostToApiWithFailMessage<InstagramContainer>(uri, null, "Failed to get reel media");
    }
    
    public async Task<Result<InstagramMediaInsightsData?>> GetMediaInsights(string instagramMediaId, string accessToken, InstagramMediaInsightType insightType)
    {
        List<string> metrics =
        [
            "comments",
            "likes",
            "reach",
            "saved",
            "shares",
            "total_interactions"
        ];

        List<string> reelMetrics =
        [
            "clips_replays_count",
            "plays",
            "ig_reels_aggregated_all_plays_count",
            "ig_reels_avg_watch_time",
            "ig_reels_video_view_total_time"
        ];

        List<string> postMetrics =
        [
            "impressions",
            "follows",
            "profile_activity",
            "profile_visits"
        ];
        
        metrics.AddRange(insightType switch
        {
            InstagramMediaInsightType.REEL => reelMetrics,
            InstagramMediaInsightType.POST => postMetrics,
            _ => []
        });
        
        var queryParams = new Dictionary<string, string?>
        {
            ["metric"] = string.Join(",", metrics),
            ["access_token"] = accessToken
        };
        
        var uri = BuildUri($"{instagramMediaId}/insights", queryParams);
        return await GetFromApiWithFailMessage<InstagramMediaInsightsData>(uri, "Failed to get post insights");
    }
}