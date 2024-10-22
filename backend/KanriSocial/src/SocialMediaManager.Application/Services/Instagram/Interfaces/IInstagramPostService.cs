using FluentResults;
using SocialMediaManager.Domain.Dtos.Instagram;
using SocialMediaManager.Domain.Models.Instagram.User;
using SocialMediaManager.Shared.Dtos.Instagram;

namespace SocialMediaManager.Application.Services.Instagram.Interfaces;

public interface IInstagramPostService
{
    Task<Result<Guid>> SchedulePost(InstagramPostDto post, InstagramUser instagramUser);
    Task PublishPost(InstagramPostDto post, InstagramContainer media, InstagramUser instagramUser);
    Task<IEnumerable<InstagramMediaDetail>> GetPostMedia(Guid userId, int pageNumber, int pageSize);
    Task<IEnumerable<InstagramComment>> GetPostComments(string mediaId, Guid userId);
    Task<IEnumerable<InstagramComment>> GetCommentReplies(string commentId, Guid userId);
    Task<Result<InstagramCommentReply?>> ReplyToComment(string commentId, string message, Guid userId);
    Task<Result<Guid>> ScheduleReel(InstagramReelDto reel, InstagramUser instagramUser);
    Task PublishReel(InstagramReelDto reel, InstagramContainer media, InstagramUser instagramUser);
}