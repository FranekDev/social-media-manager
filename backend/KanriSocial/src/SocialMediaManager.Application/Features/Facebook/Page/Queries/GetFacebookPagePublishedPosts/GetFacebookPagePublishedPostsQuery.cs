using FluentResults;
using MediatR;
using SocialMediaManager.Shared.Dtos.Facebook;

namespace SocialMediaManager.Application.Features.Facebook.Page.Queries.GetFacebookPagePublishedPosts;

public record GetFacebookPagePublishedPostsQuery(string PageId, Guid UserId) : IRequest<Result<IEnumerable<FacebookPublishedPost>>>;