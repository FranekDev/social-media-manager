using FluentResults;
using KanriSocial.Domain.Dtos.Instagram;
using KanriSocial.Infrastructure.Clients.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace KanriSocial.Infrastructure.Clients;

public class InstagramClient(IHttpClientFactory httpClientFactory, IConfiguration configuration) : IInstagramClient
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(nameof(InstagramClient));
    private readonly IConfiguration _configuration = configuration;
    // private string _accessToken = string.Empty;

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
        var response = await _httpClient.GetAsync(uri);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Fail("Failed to get long-lived token");
        }

        var content = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonConvert.DeserializeObject<InstagramTokenResponse>(content);

        return Result.Ok(tokenResponse);
    }

    public async Task<Result<InstagramMediaContainer>> GetMedia(string instagramUserId, string imageUrl, string? caption, string accessToken)
    {
        var queryParams = new Dictionary<string, string?>
        {
            ["image_url"] = imageUrl,
            ["caption"] = caption,
            ["access_token"] = accessToken
        };

        var uri = BuildUri($"{instagramUserId}/media", queryParams);
        var response = await _httpClient.PostAsync(uri, null);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Fail("Failed to get media");
        }

        var content = await response.Content.ReadAsStringAsync();
        var mediaContainer = JsonConvert.DeserializeObject<InstagramMediaContainer>(content);

        return Result.Ok(mediaContainer);
    }

    public async Task<Result> PublishMedia(string instagramUserId, InstagramMediaContainer container, string accessToken)
    {
        var queryParams = new Dictionary<string, string?>
        {
            ["creation_id"] = container.Id,
            ["access_token"] = accessToken
        };

        var uri = BuildUri($"{instagramUserId}/media_publish", queryParams);
        var response = await _httpClient.PostAsync(uri, null);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Fail("Failed to publish media");
        }

        return Result.Ok();
    }

    private string BuildUri(string path, IDictionary<string, string?> queryParams)
    {
        var basePath = _httpClient.BaseAddress?.AbsolutePath.TrimEnd('/') ?? string.Empty;
        var fullPath = $"{basePath}/{path.TrimStart('/')}";

        var uriBuilder = new UriBuilder(_httpClient.BaseAddress)
        {
            Path = fullPath
        };

        var query = QueryHelpers.AddQueryString(string.Empty, queryParams);
        uriBuilder.Query = query.TrimStart('?');

        return uriBuilder.Uri.ToString();
    }

    // public void SetAccessToken(string accessToken)
    // {
    //     _accessToken = accessToken;
    // }

}