using FluentResults;
using Hangfire;
using SocialMediaManager.Application.Extensions;
using SocialMediaManager.Application.Services.Facebook.Interfaces;
using SocialMediaManager.Domain.Models.Facebook;
using SocialMediaManager.Infrastructure.Clients.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.Facebook.Interfaces;
using SocialMediaManager.Shared.Dtos.Facebook;

namespace SocialMediaManager.Application.Services.Facebook;

public class FacebookService(
    IFacebookUserRepository facebookUserRepository, 
    IFacebookClient facebookClient,
    IFacebookFeedRepository facebookFeedRepository) : IFacebookService
{
    private readonly IFacebookUserRepository _facebookUserRepository = facebookUserRepository;
    private readonly IFacebookClient _facebookClient = facebookClient;
    private readonly IFacebookFeedRepository _facebookFeedRepository = facebookFeedRepository;
    
    public async Task<Result<FacebookPageData?>> GetPageData(FacebookUser user, string pageId)
    {
        var pageDataResult = await _facebookClient.GetPageData(pageId, user.Token);
        if (pageDataResult.IsFailed)
        {
            return Result.Fail(pageDataResult.Errors);
        }
        
        return pageDataResult?.Value;
    }

    public async Task<Result<FacebookPagePost?>> PublishPagePost(FacebookFeedPostDto feedPost, string pageAccessToken)
    {
        if (feedPost.Id is null)
        {
            return Result.Fail("Feed post id is null");
        }
        
        var pagePostResult = await _facebookClient.PublishPagePost(feedPost.PageId, feedPost.Message, pageAccessToken);
        if (pagePostResult.IsFailed)
        {
            return Result.Fail(pagePostResult.Errors);
        }
        
        var feedPostToUpdate = await _facebookFeedRepository.GetById(feedPost.Id.Value);

        if (pagePostResult?.Value?.Id is null)
        {
            return Result.Fail("Page post id is null");
        }
        
        if (feedPostToUpdate is not null)
        {
            feedPostToUpdate.IsPublished = true;
            feedPostToUpdate.PagegPostId = pagePostResult?.Value?.Id!;
            await _facebookFeedRepository.Update(feedPostToUpdate);
        }
        return pagePostResult?.Value;
    }

    public async Task<Result<IEnumerable<FacebookAccountData>>> GetAccountData(FacebookUser user)
    {
        var accountDataResult = await _facebookClient.GetAccountData(user.AccountId, user.Token);
        if (accountDataResult.IsFailed)
        {
            return Result.Fail(accountDataResult.Errors);
        }
        
        return accountDataResult?.Value?.Data ?? [];
    }

    public async Task<Result<Guid>> SchedulePagePost(FacebookFeedPostDto feedPost, FacebookUser user, string accessToken)
    {
        if (!feedPost.ScheduledAt.IsUtcDateIfNotConvertToUtc(out var scheduledAtUtc))
        {
            return Result.Fail("Failed to parse scheduled time");
        }
        
        var delay = feedPost.ScheduledAt - DateTime.UtcNow;
        
        var newFeedPost = new FacebookFeedPost
        {
            FacebookUserId = user.Id,
            Message = feedPost.Message,
            ScheduledAt = feedPost.ScheduledAt,
            IsPublished = false,
            PageId = feedPost.PageId
        };
        
        var feedPostId = await _facebookFeedRepository.Create(newFeedPost);
        var feedPostToSchedule = feedPost with { Id = feedPostId };
        
        BackgroundJob.Schedule<IFacebookService>(x => x.PublishPagePost(feedPostToSchedule, accessToken), delay);
        
        return feedPostId;
    }

    public async Task<Result<string>> GetPageAccessToken(FacebookUser user, string pageId)
    {
        var accountDataResult = await GetAccountData(user);
        if (accountDataResult.IsFailed)
        {
            return Result.Fail(accountDataResult.Errors);
        }
        
        var pageAccessToken = accountDataResult?.Value?.FirstOrDefault(x => x.Id == pageId)?.AccessToken;
        if (pageAccessToken is null)
        {
            return Result.Fail("Page access token not found");
        }
        
        return pageAccessToken;
    }

    public async Task<Result<IEnumerable<FacebookPublishedPost>>> GetPublishedPosts(string pageId, string pageAccessToken)
    {
        var publishedPostsResult = await _facebookClient.GetPublishedPostData(pageId, pageAccessToken);
        if (publishedPostsResult.IsFailed)
        {
            return Result.Fail(publishedPostsResult.Errors);
        }
        
        return Result.Ok(publishedPostsResult?.Value?.Data ?? []);
    }

    public async Task<Result<IEnumerable<FacebookPagePostComment>>> GetPostComments(string postId, string accessToken)
    {
        var postCommentsResult = await _facebookClient.GetPostComments(postId, accessToken);
        if (postCommentsResult.IsFailed)
        {
            return Result.Fail(postCommentsResult.Errors);
        }
        
        return Result.Ok(postCommentsResult?.Value?.Data ?? []);
    }
}