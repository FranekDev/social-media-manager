using FluentResults;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace KanriSocial.Infrastructure.Clients;

public abstract class BaseHttpClient(IHttpClientFactory httpClientFactory, string clientName)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(clientName);
    
    protected string BuildUri(string path, IDictionary<string, string?>? queryParams)
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
    
    private Result<T?> DeserializeOrFail<T>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            return Result.Fail<T?>("Failed to get response");
        }

        var content = response.Content.ReadAsStringAsync().Result;
        var result = JsonConvert.DeserializeObject<T>(content);

        return Result.Ok(result);
    }
    
    protected async Task<Result<T?>> GetFromApiWithFailMessage<T>(string uri, string failMessage)
    {
        var response = await _httpClient.GetAsync(uri);
        return DeserializeOrFail<T>(response);
    }
    
    protected async Task<Result<T?>> PostToApiWithFailMessage<T>(string uri, HttpContent? content, string failMessage)
    {
        var response = await _httpClient.PostAsync(uri, content);
        return DeserializeOrFail<T>(response);
    }
}