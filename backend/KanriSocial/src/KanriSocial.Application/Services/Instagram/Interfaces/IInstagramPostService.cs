using FluentResults;
using KanriSocial.Domain.Dtos.Instagram;
using KanriSocial.Domain.Models.Instagram.Post;
using KanriSocial.Domain.Models.Instagram.User;

namespace KanriSocial.Application.Services.Instagram.Interfaces;

public interface IInstagramPostService
{
    Task<Result<Guid>> SchedulePost(InstagramPostDto post);
    Task PublishPost(InstagramPostDto post, InstagramMediaContainer media, InstagramUser instagramUser);
}