using System.Text;
using FluentResults;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SocialMediaManager.Infrastructure.Clients.Interfaces;
using SocialMediaManager.Shared.Dtos.Facebook;

namespace SocialMediaManager.Infrastructure.Clients;

public class FacebookClient(IHttpClientFactory httpClientFactory, IConfiguration configuration) 
    : BaseHttpClient(httpClientFactory, nameof(FacebookClient)), IFacebookClient
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(nameof(FacebookClient));
    private readonly IConfiguration _configuration = configuration;

    public async Task<Result<FacebookAccountResponse?>> GetAccountData(string accountId, string accessToken)
    {
        var queryParams = new Dictionary<string, string?>
        {
            ["access_token"] = accessToken
        };

        var uri = BuildUri($"{accountId}/accounts", queryParams);
        return await GetFromApiWithFailMessage<FacebookAccountResponse>(uri, "Failed to retrieve Facebook account data.");
    }

    public async Task<Result<FacebookPageData?>> GetPageData(string pageId, string accessToken)
    {
        List<string> fields =
        [
            "id",
            "about",
            "category",
            "category_list",
            "checkins",
            "connected_instagram_account",
            "cover",
            "current_location",
            "description",
            "followers_count",
            "is_published",
            "link",
            "name",
            "rating_count"
        ];
        
        var queryParams = new Dictionary<string, string?>
        {
            ["fields"] = string.Join(",", fields),
            ["access_token"] = accessToken
        };

        var uri = BuildUri(pageId, queryParams);
        return await GetFromApiWithFailMessage<FacebookPageData>(uri, "Failed to retrieve Facebook page data.");
    }
    
    public async Task<Result<FacebookPagePost?>> PublishPagePost(string pageId, string message, string accessToken)
    {
        var uri = BuildUri($"{pageId}/feed", null);
        var content = new StringContent(JsonConvert.SerializeObject(new
        {
            message, 
            access_token = accessToken
        }), Encoding.UTF8, "application/json");
        
        return await PostToApiWithFailMessage<FacebookPagePost>(uri, content, "Failed to publish Facebook page post.");
    }

    public async Task<Result<FacebookPublishedPostData?>> GetPublishedPostData(string pageId, string accessToken)
    {
        var queryParams = new Dictionary<string, string?>
        {
            ["access_token"] = accessToken
        };
        
        var uri = BuildUri($"{pageId}/published_posts", queryParams);
        return await GetFromApiWithFailMessage<FacebookPublishedPostData>(uri, "Failed to retrieve Facebook published post data.");
    }

    public async Task<Result<FacebookPagePostCommentData?>> GetPostComments(string postId, string accessToken)
    {
        var queryParams = new Dictionary<string, string?>
        {
            ["access_token"] = accessToken
        };
        
        var uri = BuildUri($"{postId}/comments", queryParams);
        return await GetFromApiWithFailMessage<FacebookPagePostCommentData>(uri, "Failed to retrieve Facebook post comments.");
    }

    public async Task<Result<FacebookData<FacebookPagePicture>?>> GetPagePicture(string pageId, string accessToken)
    {
        var queryParams = new Dictionary<string, string?>
        {
            ["redirect"] = 0.ToString(),
            ["access_token"] = accessToken
        };

        var uri = BuildUri($"{pageId}/picture", queryParams);

        return await GetFromApiWithFailMessage<FacebookData<FacebookPagePicture>>(uri, "Nie udało się pobrać zdjęcia strony.");
    }

    public async Task<Result<FacebookNewComment?>> CreatePagePostComment(string pagePostId, string message, string accessToken)
    {
        var uri = BuildUri($"{pagePostId}/comments", null);
        var content = new StringContent(JsonConvert.SerializeObject(new
        {
            message, 
            access_token = accessToken
        }), Encoding.UTF8, "application/json");
        
        return await PostToApiWithFailMessage<FacebookNewComment>(uri, content, "Nie udało się dodać komentarza do posta.");
    }

    public async Task<Result<FacebookAccountId?>> GetAccountId(string accessToken)
    {
        var queryParams = new Dictionary<string, string?>
        {
            ["fields"] = "id",
            ["access_token"] = accessToken
        };

        var uri = BuildUri("me", queryParams);
        return await GetFromApiWithFailMessage<FacebookAccountId>(uri, "Nie udało się pobrać ID konta.");
    }
}