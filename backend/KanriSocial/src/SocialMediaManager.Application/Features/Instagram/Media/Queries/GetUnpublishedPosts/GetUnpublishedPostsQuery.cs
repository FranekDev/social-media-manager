using FluentResults;
using MediatR;
using SocialMediaManager.Domain.Dtos.Instagram;

namespace SocialMediaManager.Application.Features.Instagram.Media.Queries.GetUnpublishedPosts;

public record GetUnpublishedPostsQuery(Guid UserId) : IRequest<Result<IEnumerable<InstagramPostDto>>>;