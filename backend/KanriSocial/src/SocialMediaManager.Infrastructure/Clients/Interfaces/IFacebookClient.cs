using FluentResults;
using SocialMediaManager.Shared.Dtos.Facebook;

namespace SocialMediaManager.Infrastructure.Clients.Interfaces;

public interface IFacebookClient
{
    /// <summary>
    /// Retrieves account data for a given Facebook account.
    /// </summary>
    /// <param name="accountId">The ID of the Facebook account.</param>
    /// <param name="accessToken">The access token for authenticating the request.</param>
    /// <returns>The task result contains a Result object wrapping a FacebookAccountResponse.</returns>
    Task<Result<FacebookAccountResponse?>> GetAccountData(string accountId, string accessToken);
 
    /// <summary>
    /// Retrieves page data for a given Facebook page.
    /// </summary>
    /// <param name="pageId">The ID of the Facebook page.</param>
    /// <param name="accessToken">The access token for authenticating the request.</param>
    /// <returns>The task result contains a Result object wrapping a FacebookPageData.</returns>
    Task<Result<FacebookPageData?>> GetPageData(string pageId, string accessToken);
    
    /// <summary>
    /// Publishes a post to a specified Facebook page.
    /// </summary>
    /// <param name="pageId">The ID of the Facebook page.</param>
    /// <param name="message">The message content of the post.</param>
    /// <param name="accessToken">The access token for authenticating the request.</param>
    /// <returns>The task result contains a Result object wrapping a FacebookPagePost.</returns>
    Task<Result<FacebookPagePost?>> PublishPagePost(string pageId, string message, string accessToken);
    
    /// <summary>
    /// Retrieves published post data for a given Facebook page.
    /// </summary>
    /// <param name="pageId">The ID of the Facebook page.</param>
    /// <param name="accessToken">The access token for authenticating the request.</param>
    /// <returns>The task result contains a Result object wrapping a FacebookPublishedPostData.</returns>
    Task<Result<FacebookPublishedPostData?>> GetPublishedPostData(string pageId, string accessToken);
 
    /// <summary>
    /// Retrieves comments for a given Facebook post.
    /// </summary>
    /// <param name="postId">The ID of the Facebook post.</param>
    /// <param name="accessToken">The access token for authenticating the request.</param>
    /// <returns>The task result contains a Result object wrapping a FacebookPagePostCommentData.</returns>
    Task<Result<FacebookPagePostCommentData?>> GetPostComments(string postId, string accessToken);

    Task<Result<FacebookData<FacebookPagePicture>?>> GetPagePicture(string pageId, string accessToken);
    Task<Result<FacebookNewComment?>> CreatePagePostComment(string pagePostId, string message, string accessToken);
}