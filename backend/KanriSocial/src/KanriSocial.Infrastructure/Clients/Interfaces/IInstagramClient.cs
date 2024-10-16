using FluentResults;
using KanriSocial.Domain.Dtos.Instagram;

namespace KanriSocial.Infrastructure.Clients.Interfaces;

public interface IInstagramClient
{
    /// <summary>
    /// Refresh the user's Instagram access token.
    /// </summary>
    /// <param name="accessToken"></param>
    /// <returns>The task result contains the refreshed Instagram token response.</returns>
    Task<Result<InstagramTokenResponse?>> GetLongLivedToken(string accessToken);

    /// <summary>
    /// Get media to be published on the user's Instagram account.
    /// </summary>
    /// <param name="instagramUserId">Instagram user's account Id</param>
    /// <param name="imageUrl">Image url for post to be uploaded</param>
    /// <param name="caption">Post caption</param>
    /// <param name="accessToken">User's access token</param>
    /// <returns>The task result contains the Instagram media container which contains container Id</returns>
    Task<Result<InstagramMediaContainer>> GetMedia(string instagramUserId, string imageUrl, string? caption, string accessToken);

    /// <summary>
    /// Publish media to the user's Instagram account.
    /// </summary>
    /// <param name="instagramUserId">Instagram user's account Id</param>
    /// <param name="container">Container with post Id to be published</param>
    /// <param name="accessToken">User's access token</param>
    /// <returns>The task result indicates whether the operation was successful.</returns>
    Task<Result> PublishMedia(string instagramUserId, InstagramMediaContainer container, string accessToken);
}