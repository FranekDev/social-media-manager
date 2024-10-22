using FluentResults;
using SocialMediaManager.Domain.Dtos.Instagram;
using MediatR;

namespace SocialMediaManager.Application.Features.Instagram.Post.Queries.GetUnpublishedPosts;

public record GetUnpublishedPostsQuery(Guid UserId) : IRequest<Result<IEnumerable<InstagramPostDto>>>;