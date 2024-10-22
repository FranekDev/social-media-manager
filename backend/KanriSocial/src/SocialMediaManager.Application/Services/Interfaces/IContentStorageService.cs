namespace SocialMediaManager.Application.Services.Interfaces;

public interface IContentStorageService
{
    Task<string> UploadInstagramPostAndGetUrl(Stream stream, string path);
}