using MediatR;
using SocialMediaManager.Shared.Dtos.Instagram;

namespace SocialMediaManager.Application.Features.Instagram.Media.Queries.GetUnpublishedReels;

public record GetUnpublishedReelsQuery(Guid UserId) : IRequest<IEnumerable<InstagramReelDto>>;