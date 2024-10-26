using FluentResults;
using MediatR;
using SocialMediaManager.Shared.Dtos.Facebook;

namespace SocialMediaManager.Application.Features.Facebook.Page.Queries.GetFacebookPagePostComments;

public record GetFacebookPagePostCommentsQuery(string PageId, string PostId, Guid UserId) : IRequest<Result<IEnumerable<FacebookPagePostComment>>>;