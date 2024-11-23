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
using SocialMediaManager.Shared.Enums.Instagram;

namespace SocialMediaManager.Application.Services.Instagram;

public class InstagramMediaService(
    IInstagramClient instagramClient, 
    IInstagramUserRepository instagramUserRepository,
    IInstagramPostRepository instagramPostRepository,
    IInstagramReelRepository instagramReelRepository) : IInstagramMediaService
{
    private readonly IInstagramClient _instagramClient = instagramClient;
    private readonly IInstagramUserRepository _instagramUserRepository = instagramUserRepository;
    private readonly IInstagramPostRepository _instagramPostRepository = instagramPostRepository;
    private readonly IInstagramReelRepository _instagramReelRepository = instagramReelRepository;

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
            ScheduledAt = scheduledTimeUtc,
            IsPublished = false,
            ContainerId = mediaResult.Value.Id
        };
        var newPostId = await _instagramPostRepository.Create(instagramPost);
        
        BackgroundJob.Schedule<IInstagramMediaService>(x => x.PublishPost(post, mediaResult.Value, instagramUser), delay);
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

    public async Task<Result<Guid>> ScheduleReel(InstagramReelDto reel, InstagramUser instagramUser)
    {
        var mediaRsult = await _instagramClient.GetReelMedia(instagramUser.AccountId, reel.VideoUrl, reel.Caption, instagramUser.Token);
        if (mediaRsult.IsFailed)
        {
            return Result.Fail("Failed to get reel media");
        }
        
        if (!IsUtcDateIfNotConvertToUtc(reel?.ScheduledAt ?? DateTime.UtcNow, out var scheduledTimeUtc))
        {
            return Result.Fail("Failed to parse scheduled time");
        }
        
        var delay = scheduledTimeUtc - DateTime.UtcNow;

        var instagramReel = new InstagramReel
        {
            InstagramUser = instagramUser,
            VideoUrl = reel.VideoUrl,
            Caption = reel.Caption,
            ScheduledAt = reel.ScheduledAt,
            ContainerId = mediaRsult.Value?.Id
        };

        var newReelId = await _instagramReelRepository.Create(instagramReel);
        reel.Id = newReelId;
        
        BackgroundJob.Schedule<IInstagramMediaService>(x => x.PublishReel(reel, mediaRsult.Value, instagramUser), delay);
        
        return Result.Ok(newReelId);
    }
    
    public async Task PublishReel(InstagramReelDto reel, InstagramContainer media, InstagramUser instagramUser)
    {
        var result = await _instagramClient.PublishMedia(instagramUser.AccountId, media, instagramUser.Token);

        if (result.IsFailed)
        {
            return;
        }
        
        var reelToUpdate = await _instagramReelRepository.GetById(reel.Id);
        if (reelToUpdate is not null)
        {
            reelToUpdate.IsPublished = true;
            reelToUpdate.MediaId = result.Value?.Id;
            await _instagramReelRepository.Update(reelToUpdate);
        }
    }
    
    public async Task<Result<InstagramMediaInsightsData?>> GetMediaInsights(
        string mediaId, 
        InstagramUser instagramUser, 
        InstagramMediaInsightType insightType)
    {
        var result = await _instagramClient.GetMediaInsights(mediaId, instagramUser.Token, insightType);
        return result;
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