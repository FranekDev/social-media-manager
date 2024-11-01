using FluentResults;
using MediatR;
using SocialMediaManager.Shared.Dtos.TikTok;

namespace SocialMediaManager.Application.Features.TikTok.Queries.GetTikTokUserInfo;

public record GetTikTokUserInfoQuery(Guid UserId) : IRequest<Result<TikTokUserInfo>>;