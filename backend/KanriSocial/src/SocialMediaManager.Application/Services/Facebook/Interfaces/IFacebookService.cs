using FluentResults;
using SocialMediaManager.Domain.Models.Facebook;
using SocialMediaManager.Shared.Dtos.Facebook;

namespace SocialMediaManager.Application.Services.Facebook.Interfaces;

public interface IFacebookService
{
    Task<Result<FacebookPageData?>> GetPageData(FacebookUser user,string pageId); 
    Task<Result<FacebookPagePost?>> PublishPagePost(FacebookFeedPostDto feedPost, string pageAccessToken);
    Task<Result<IEnumerable<FacebookAccountData>>> GetAccountData(FacebookUser user);
    Task<Result<Guid>> SchedulePagePost(FacebookFeedPostDto feedPost, FacebookUser user, string accessToken);
    Task<Result<IEnumerable<FacebookPublishedPost>>> GetPublishedPosts(string pageId, string pageAccessToken);
    Task<Result<string>> GetPageAccessToken(FacebookUser user, string pageId);
    Task<Result<IEnumerable<FacebookPagePostComment>>> GetPostComments(string postId, string accessToken);
    Task<Result<IEnumerable<FacebookUserPage>>> GetUserPages(FacebookUser user);
}