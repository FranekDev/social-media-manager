using FluentResults;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace SocialMediaManager.Infrastructure.Clients;

public abstract class BaseHttpClient(IHttpClientFactory httpClientFactory, string clientName)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(clientName);
    
    protected string BuildUri(string path, IDictionary<string, string?>? queryParams = null)
    {
        var basePath = _httpClient.BaseAddress?.AbsolutePath.TrimEnd('/') ?? string.Empty;
        var fullPath = $"{basePath}/{path.TrimStart('/')}";
        
        if (_httpClient.BaseAddress is null)
        {
            throw new InvalidOperationException("Http client base address is not set");
        }

        var uriBuilder = new UriBuilder(_httpClient.BaseAddress)
        {
            Path = fullPath
        };

        if (queryParams is not null)
        {
            var query = QueryHelpers.AddQueryString(string.Empty, queryParams);
            uriBuilder.Query = query.TrimStart('?');
        }

        return uriBuilder.Uri.ToString();
    }
    
    private async Task<Result<T?>> DeserializeOrFail<T>(HttpResponseMessage response, string? failMessage = "Failed to get data from API")
    {
        if (!response.IsSuccessStatusCode)
        {
            return Result.Fail(string.IsNullOrEmpty(failMessage) ? response.ReasonPhrase : failMessage);
        }

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<T>(content);

        return result;
    }
    
    protected async Task<Result<T?>> GetFromApiWithFailMessage<T>(string uri, string? failMessage = null)
    {
        var response = await _httpClient.GetAsync(uri);
        return await DeserializeOrFail<T>(response, failMessage);
    }
    
    protected async Task<Result<T?>> GetFromApiWithFailMessage<T>(HttpRequestMessage request, string? failMessage = null)
    {
        var response = await _httpClient.SendAsync(request);
        return await DeserializeOrFail<T>(response, failMessage);
    }
    
    protected async Task<Result<T?>> PostToApiWithFailMessage<T>(string uri, HttpContent? content, string? failMessage = null)
    { 
        var response = await _httpClient.PostAsync(uri, content);
        return await DeserializeOrFail<T>(response, failMessage);
    }
    
    protected async Task<Result<T?>> PostToApiWithFailMessage<T>(HttpRequestMessage request, string? failMessage = null)
    {
        var response = await _httpClient.SendAsync(request);
        return await DeserializeOrFail<T>(response, failMessage);
    }
}