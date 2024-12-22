using MediatR;
using SocialMediaManager.Shared.Dtos.Facebook;

namespace SocialMediaManager.Application.Features.Facebook.Page.Queries.GetFacebookScheduledPosts;

public record GetFacebookScheduledPostsQuery(Guid UserId) : IRequest<IEnumerable<FacebookFeedPostDto>>;