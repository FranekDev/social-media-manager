using BunnyCDN.Net.Storage;
using Microsoft.Extensions.Configuration;
using SocialMediaManager.Application.Services.Interfaces;

namespace SocialMediaManager.Application.Services;

public class ContentStorageService : IContentStorageService
{
    private readonly BunnyCDNStorage _bunnyCDNStorage;
    private readonly string _baseURl;
    private readonly string _storageZoneName;
    
    public ContentStorageService(IConfiguration configuration)
    {
        _storageZoneName = configuration["BunnyCDN:StorageZoneName"];
        _baseURl = configuration["BunnyCDN:BaseUrl"];
        var apiAccessKey = configuration["BunnyCDN:ApiAccessKey"];
        _bunnyCDNStorage = new BunnyCDNStorage(_storageZoneName, apiAccessKey);
    }

    public async Task<string> UploadInstagramPostAndGetUrl(Stream stream, string path)
    {
        await Upload(stream, $"Instagram/Posts/{path}");
        return $"{_baseURl}/Instagram/Posts/{path}";
    }
    
    public async Task<string> UploadInstagramReelAndGetUrl(Stream stream, string path)
    {
        await Upload(stream, $"Instagram/Reels/{path}");
        return $"{_baseURl}/Instagram/Reels/{path}";
    }
    
    public async Task Upload(Stream stream, string path)
    {
        await _bunnyCDNStorage.UploadAsync(stream, $"{_storageZoneName}/{path}");
    }
}