using System.Runtime.InteropServices.Marshalling;
using FluentResults;
using SocialMediaManager.Shared.Dtos.Instagram;
using SocialMediaManager.Shared.Enums.Instagram;

namespace SocialMediaManager.Infrastructure.Clients.Interfaces;

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
    Task<Result<InstagramContainer?>> GetMedia(string instagramUserId, string imageUrl, string? caption, string accessToken);

    /// <summary>
    /// Publish media to the user's Instagram account.
    /// </summary>
    /// <param name="instagramUserId">Instagram user's account Id</param>
    /// <param name="container">Container with post Id to be published</param>
    /// <param name="accessToken">User's access token</param>
    /// <returns>The task result indicates whether the operation was successful.</returns>
    Task<Result<InstagramMedia?>> PublishMedia(string instagramUserId, InstagramContainer container, string accessToken);
    
    /// <summary>
    /// Get user details from Instagram.
    /// </summary>
    /// <param name="instagramUserId">Instagram user's Id</param>
    /// <param name="accessToken">User's access token</param>
    /// <returns>The task result contains the Instagram User Detail object</returns>
    Task<Result<InstagramUserDetail?>> GetUserDetail(string instagramUserId,string accessToken);
    
    /// <summary>
    /// Get media details from Instagram.
    /// </summary>
    /// <param name="instagramMediaId">Instagram media Id</param>
    /// <param name="accessToken">User's access token</param>
    /// <returns>The task result contains the Instagram Media object</returns>
    Task<Result<InstagramMediaDetail?>> GetMediaDetail(string instagramMediaId, string accessToken);
    
    /// <summary>
    /// Get media comments from Instagram.
    /// </summary>
    /// <param name="instagramMediaId">Instagram media Id</param>
    /// <param name="accessToken">User's access token</param>
    /// <returns>The task result contains Instagram Media comments</returns>
    Task<Result<InstagramCommentData?>> GetMediaComments(string instagramMediaId, string accessToken);
    
    /// <summary>
    /// Get comment replies from Instagram.
    /// </summary>
    /// <param name="commentId">Instagram comment Id</param>
    /// <param name="accessToken">User's access token</param>
    /// <returns>The task result contains Instagram comment replies</returns>
    Task<Result<InstagramCommentData?>> GetCommentReplies(string commentId, string accessToken);
    
    /// <summary>
    /// Reply to a comment on Instagram.
    /// </summary>
    /// <param name="commentId">Instagram comment Id</param>
    /// <param name="accessToken">User's access token</param>
    /// <param name="message">Message</param>
    /// <returns>The task result contains Instagram comment reply Id</returns>
    Task<Result<InstagramCommentReply?>> ReplyToComment(string commentId, string accessToken, string message);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="instagramUserId"></param>
    /// <param name="videoUrl"></param>
    /// <param name="caption"></param>
    /// <param name="accessToken"></param>
    /// <returns></returns>
    Task<Result<InstagramContainer?>> GetReelMedia(string instagramUserId, string videoUrl, string? caption, string accessToken);
 
    /// <summary>
    /// Get insights data for a specific Instagram media (post or reel).
    /// </summary>
    /// <param name="instagramMediaId">Instagram media Id</param>
    /// <param name="accessToken">User's access token</param>
    /// <param name="insightType">Type of Instagram media insight (post or reel)</param>
    /// <returns>The task result contains the Instagram media insights data</returns>
    Task<Result<InstagramMediaInsightsData?>> GetMediaInsights(string instagramMediaId, string accessToken, InstagramMediaInsightType insightType);
}