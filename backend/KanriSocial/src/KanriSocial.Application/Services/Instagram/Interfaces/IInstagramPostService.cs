using FluentResults;
using KanriSocial.Domain.Dtos.Instagram;
using KanriSocial.Domain.Models.Instagram.Post;
using KanriSocial.Domain.Models.Instagram.User;
using KanriSocial.Shared.Dtos.Instagram;

namespace KanriSocial.Application.Services.Instagram.Interfaces;

public interface IInstagramPostService
{
    Task<Result<Guid>> SchedulePost(InstagramPostDto post, InstagramUser instagramUser);
    Task PublishPost(InstagramPostDto post, InstagramContainer media, InstagramUser instagramUser);
    Task<IEnumerable<InstagramMediaDetail>> GetPostMedia(Guid userId, int pageNumber, int pageSize);
    Task<IEnumerable<InstagramComment>> GetPostComments(string mediaId, Guid userId);
    Task<IEnumerable<InstagramComment>> GetCommentReplies(string commentId, Guid userId);
}