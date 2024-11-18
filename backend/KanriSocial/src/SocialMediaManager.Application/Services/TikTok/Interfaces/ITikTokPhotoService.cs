using FluentResults;
using SocialMediaManager.Shared.Dtos.TikTok;
using TikTokUser = SocialMediaManager.Domain.Models.TikTok.TikTokUser;

namespace SocialMediaManager.Application.Services.TikTok.Interfaces;

public interface ITikTokPhotoService
{
    Task<Result<Guid>> SchedulePhoto(TikTokContent content, TikTokUser tikTokUser, Guid photoId);
}