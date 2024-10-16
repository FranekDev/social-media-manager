using KanriSocial.Infrastructure.Clients.Interfaces;
using Microsoft.Extensions.Configuration;

namespace KanriSocial.Infrastructure.Clients.Builders;

public class InstagramClientBuilder(IHttpClientFactory httpClientFactory, IConfiguration configuration)
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly IConfiguration _configuration = configuration;
    private string? _accessToken;
    
    public InstagramClientBuilder WithAccessToken(string accessToken)
    {
        _accessToken = accessToken;
        return this;
    }

    public IInstagramClient Build()
    {
        var client = new InstagramClient(_httpClientFactory, _configuration);
        if (_accessToken != null)
        {
            // client.SetAccessToken(_accessToken);
        }
        return client;
    }
}