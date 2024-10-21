using FluentResults;
using KanriSocial.Shared.Dtos.Instagram;
using MediatR;

namespace KanriSocial.Application.Features.Instagram.Comment.Queries.GetInstagramPostComments;

public record GetInstagramPostCommentsQuery(string MediaId, Guid UserId) : IRequest<Result<IEnumerable<InstagramComment>>>;