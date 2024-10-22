using FluentResults;
using SocialMediaManager.Shared.Dtos.Instagram;
using MediatR;

namespace SocialMediaManager.Application.Features.Instagram.Comment.Queries.GetInstagramPostComments;

public record GetInstagramPostCommentsQuery(string MediaId, Guid UserId) : IRequest<Result<IEnumerable<InstagramComment>>>;