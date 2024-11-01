using System.Net.Http.Headers;
using FluentResults;
using Microsoft.Extensions.Configuration;
using SocialMediaManager.Infrastructure.Clients.Interfaces;
using SocialMediaManager.Shared.Dtos.TikTok;
using SocialMediaManager.Shared.Enums.TikTok;

namespace SocialMediaManager.Infrastructure.Clients;

public class TikTokClient(IHttpClientFactory clientFactory, IConfiguration configuration) 
    : BaseHttpClient(clientFactory, nameof(TikTokClient)), ITikTokClient
{
    private readonly HttpClient _httpClient = clientFactory.CreateClient(nameof(TikTokClient));
    private readonly IConfiguration _configuration = configuration;
    
    public async Task<Result<Response<TikTokUserInfo>?>> GetUserInfo(string accessToken)
    {
        List<string> fields = ["open_id", "union_id", "avatar_url", "display_name"];
        var queryParams = new Dictionary<string, string?>
        {
            ["fields"] = string.Join(",", fields)
        };
        var uri = BuildUri("user/info/", queryParams);
        
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        
        return await GetFromApiWithFailMessage<Response<TikTokUserInfo>>(request, "Failed to retrieve TikTok user info.");
    }

    public async Task<Result<TikTokToken?>> GetToken(string token, TikTokTokenType tokenType)
    {
        var queryParams = GetTokenQueryParams(token, tokenType);
        var content = new FormUrlEncodedContent(queryParams);
        var uri = BuildUri("oauth/token/");
        var tokenName = tokenType == TikTokTokenType.AccessToken ? "access" : "refresh";
        
        return await PostToApiWithFailMessage<TikTokToken>(uri, content, $"Failed to retrieve TikTok {tokenName} token.");
    }

    private Dictionary<string, string?> GetTokenQueryParams(string token, TikTokTokenType tokenType)
    {
        var baseParams = new Dictionary<string, string?>
        {
            ["client_key"] = _configuration["TikTok:ClientKey"],
            ["client_secret"] = _configuration["TikTok:ClientSecret"]
        };
        
        var accessTokenParams = new Dictionary<string, string?>
        {
            ["code"] = token,
            ["grant_type"] = "authorization_code",
            ["redirect_uri"] = _configuration["TikTok:RedirectUri"]
        };
        
        var refreshTokenParams = new Dictionary<string, string?>
        {
            ["refresh_token"] = token,
            ["grant_type"] = "refresh_token"
        };
        
        var queryParams = tokenType switch
        {
            TikTokTokenType.AccessToken => baseParams.Concat(accessTokenParams).ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
            TikTokTokenType.RefreshToken => baseParams.Concat(refreshTokenParams).ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
            _ => baseParams
        };
        
        return queryParams;
    }
}