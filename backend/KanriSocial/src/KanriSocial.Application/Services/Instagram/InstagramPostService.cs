using FluentResults;
using Hangfire;
using KanriSocial.Application.Services.Instagram.Interfaces;
using KanriSocial.Domain.Dtos.Instagram;
using KanriSocial.Domain.Models.Instagram.Post;
using KanriSocial.Domain.Models.Instagram.User;
using KanriSocial.Infrastructure.Clients.Interfaces;
using KanriSocial.Infrastructure.Repositories.Instagram.Interfaces;
using KanriSocial.Infrastructure.Repositories.Instagram.Interfaces.User;

namespace KanriSocial.Application.Services.Instagram;

public class InstagramPostService(
    IInstagramClient instagramClient, 
    IInstagramUserRepository instagramUserRepository,
    IInstagramPostRepository instagramPostRepository) : IInstagramPostService
{
    private readonly IInstagramClient _instagramClient = instagramClient;
    private readonly IInstagramUserRepository _instagramUserRepository = instagramUserRepository;
    private readonly IInstagramPostRepository _instagramPostRepository = instagramPostRepository;

    public async Task<Result<Guid>> SchedulePost(InstagramPostDto post)
    {
        var instagramUser = await _instagramUserRepository.GetByUserId(post.UserId);
        if (instagramUser is null)
        {
            return Result.Fail("Instagram user not found");
        }
        
        var mediaResult = await _instagramClient.GetMedia(instagramUser.AccountId, post.ImageUrl, post?.Caption, instagramUser.Token);
        if (mediaResult.IsFailed)
        {
            return Result.Fail("Failed to get media");
        }

        var scheduledAtLocal = DateTime.SpecifyKind(post?.ScheduledAt ?? DateTime.UtcNow, DateTimeKind.Local);
        // var polandTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
        var scheduledTimeUtc = TimeZoneInfo.ConvertTimeToUtc(scheduledAtLocal, TimeZoneInfo.Local);
        
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
    
    public async Task PublishPost(InstagramPostDto post, InstagramMediaContainer media, InstagramUser instagramUser)
    {
        var result = await _instagramClient.PublishMedia(instagramUser.AccountId, media, instagramUser.Token);

        if (result.IsFailed)
        {
            return;
        }
        
        var postToUpdate = await _instagramPostRepository.GetById(post.Id);
        if (postToUpdate is not null)
        {
            _instagramPostRepository.Detach(postToUpdate);
            var updatedPost = postToUpdate with { IsPublished = true };
        
            await _instagramPostRepository.Update(updatedPost);
        }
    }
}