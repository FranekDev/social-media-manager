using FluentResults;
using SocialMediaManager.Shared.Dtos.TikTok;
using TikTokUser = SocialMediaManager.Domain.Models.TikTok.TikTokUser;

namespace SocialMediaManager.Application.Services.TikTok.Interfaces;

public interface ITikTokVideoService
{
    Task<Result<Guid>> ScheduleVideo(TikTokPostInfo postInfo, TikTokSurceInfo sourceInfo, TikTokUser tikTokUser, Guid videoId);
}