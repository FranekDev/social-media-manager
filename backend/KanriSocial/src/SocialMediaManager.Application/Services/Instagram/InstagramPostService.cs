using FluentResults;
using Hangfire;
using SocialMediaManager.Domain.Dtos.Instagram;
using SocialMediaManager.Domain.Models.Instagram.Post;
using SocialMediaManager.Domain.Models.Instagram.User;
using SocialMediaManager.Infrastructure.Clients.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.Instagram.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.Instagram.Interfaces.User;
using SocialMediaManager.Shared.Dtos.Instagram;
using SocialMediaManager.Application.Services.Instagram.Interfaces;

namespace SocialMediaManager.Application.Services.Instagram;

public class InstagramPostService(
    IInstagramClient instagramClient, 
    IInstagramUserRepository instagramUserRepository,
    IInstagramPostRepository instagramPostRepository) : IInstagramPostService
{
    private readonly IInstagramClient _instagramClient = instagramClient;
    private readonly IInstagramUserRepository _instagramUserRepository = instagramUserRepository;
    private readonly IInstagramPostRepository _instagramPostRepository = instagramPostRepository;

    public async Task<Result<Guid>> SchedulePost(InstagramPostDto post, InstagramUser instagramUser)
    {
        var mediaResult = await _instagramClient.GetMedia(instagramUser.AccountId, post.ImageUrl, post?.Caption, instagramUser.Token);
        if (mediaResult.IsFailed)
        {
            return Result.Fail("Failed to get media");
        }
        
        if (!IsUtcDateIfNotConvertToUtc(post?.ScheduledAt ?? DateTime.UtcNow, out var scheduledTimeUtc))
        {
            return Result.Fail("Failed to parse scheduled time");
        }
        
        var delay = scheduledTimeUtc - DateTime.UtcNow;

        var instagramPost = new InstagramPost
        {
            Id = post?.Id ?? Guid.NewGuid(),
            InstagramUser = instagramUser,
            ImageUrl = post.ImageUrl,
            Caption = post.Caption,
            ScheduledAt = post.ScheduledAt,
            IsPublished = false,
            ContainerId = mediaResult.Value.Id
        };
        var newPostId = await _instagramPostRepository.Create(instagramPost);
        
        BackgroundJob.Schedule<IInstagramPostService>(x => x.PublishPost(post, mediaResult.Value, instagramUser), delay);
        return Result.Ok(newPostId);
    }
    
    public async Task PublishPost(InstagramPostDto post, InstagramContainer media, InstagramUser instagramUser)
    {
        var result = await _instagramClient.PublishMedia(instagramUser.AccountId, media, instagramUser.Token);

        if (result.IsFailed)
        {
            return;
        }
        
        var postToUpdate = await _instagramPostRepository.GetById(post.Id);
        if (postToUpdate is not null)
        {
            postToUpdate.IsPublished = true;
            await _instagramPostRepository.Update(postToUpdate);
        }
    }

    public async Task<IEnumerable<InstagramMediaDetail>> GetPostMedia(Guid userId, int pageNumber, int pageSize)
    {
        // var postsData = await _instagramPostRepository.GetAll();
        var instagramUser = await _instagramUserRepository.GetByUserId(userId);

        var userDetail = await _instagramClient.GetUserDetail(instagramUser.AccountId, instagramUser.Token);

        if (userDetail?.Value?.Media.Data.Count == 0)
        {
            return [];
        }
        
        var mediaData = new List<InstagramMediaDetail>();
        
        var paginatedMedia = userDetail.Value.Media.Data.Skip(pageNumber * pageSize).Take(pageSize);
        
        foreach (var media in paginatedMedia)
        {
            var mediaResult = await _instagramClient.GetMediaDetail(media.Id, instagramUser.Token);
            if (mediaResult.IsSuccess)
            {
                mediaData.Add(mediaResult.Value);
            }
        }
        
        return mediaData;
    }

    public async Task<IEnumerable<InstagramComment>> GetPostComments(string mediaId, Guid userId)
    {
        var instagramUser = await _instagramUserRepository.GetByUserId(userId);
        
        var result = await _instagramClient.GetMediaComments(mediaId, instagramUser.Token);
        return result.Value?.Data ?? [];
    }
    
    public async Task<IEnumerable<InstagramComment>> GetCommentReplies(string commentId, Guid userId)
    {
        var instagramUser = await _instagramUserRepository.GetByUserId(userId);
        
        var result = await _instagramClient.GetCommentReplies(commentId, instagramUser.Token);
        
        // add validation is comment id is correct or return error result
        return result.Value?.Data ?? [];
    }

    public async Task<Result<InstagramCommentReply?>> ReplyToComment(string commentId, string message, Guid userId)
    {
        var instagramUser = await _instagramUserRepository.GetByUserId(userId);
        
        if (instagramUser is null)
        {
            return Result.Fail("User not found");
        }
        
        var result = await _instagramClient.ReplyToComment(commentId, instagramUser.Token, message);
        if (result.IsFailed)
        {
            return Result.Fail("Failed to reply to comment");
        }
        
        
        return Result.Ok(result.Value);
    }

    private bool IsUtcDateIfNotConvertToUtc(DateTime scheduledAt, out DateTime scheduledAtUtc)
    {
        if (scheduledAt.Kind == DateTimeKind.Utc)
        {
            scheduledAtUtc = scheduledAt;
            return true;
        }

        scheduledAtUtc = DateTime.SpecifyKind(scheduledAt, DateTimeKind.Local);
        scheduledAtUtc = TimeZoneInfo.ConvertTimeToUtc(scheduledAtUtc, TimeZoneInfo.Local);
        return true;
    }
}