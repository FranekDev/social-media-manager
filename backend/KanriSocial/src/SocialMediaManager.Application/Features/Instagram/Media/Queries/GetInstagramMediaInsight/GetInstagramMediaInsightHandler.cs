using FluentResults;
using MediatR;
using SocialMediaManager.Application.Services.Instagram.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.Instagram.Interfaces.User;
using SocialMediaManager.Shared.Dtos.Instagram;

namespace SocialMediaManager.Application.Features.Instagram.Media.Queries.GetInstagramMediaInsight;

public class GetInstagramMediaInsightHandler(
    IInstagramUserRepository instagramUserRepository,
    IInstagramMediaService instagramMediaService) : IRequestHandler<GetInstagramMediaInsightQuery, Result<InstagramMediaInsightsData?>>
{
    public async Task<Result<InstagramMediaInsightsData?>> Handle(GetInstagramMediaInsightQuery request, CancellationToken cancellationToken)
    {
        var instagramUser = await instagramUserRepository.GetByUserId(request.UserId);

        if (instagramUser == null)
        {
            return Result.Fail("User not found");
        }

        var result = await instagramMediaService.GetMediaInsights(request.MediaId, instagramUser, request.InsightType);

        return result.IsFailed ? Result.Fail(result.Errors) : Result.Ok(result.Value);
    }
}