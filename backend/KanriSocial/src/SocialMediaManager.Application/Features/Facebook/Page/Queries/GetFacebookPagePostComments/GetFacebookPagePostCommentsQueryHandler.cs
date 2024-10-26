using FluentResults;
using MediatR;
using SocialMediaManager.Application.Services.Facebook.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.Facebook.Interfaces;
using SocialMediaManager.Shared.Dtos.Facebook;

namespace SocialMediaManager.Application.Features.Facebook.Page.Queries.GetFacebookPagePostComments;

public class GetFacebookPagePostCommentsQueryHandler(
    IFacebookUserRepository facebookUserRepository,
    IFacebookService facebookService) : IRequestHandler<GetFacebookPagePostCommentsQuery, Result<IEnumerable<FacebookPagePostComment>>>
{
    private readonly IFacebookUserRepository _facebookUserRepository = facebookUserRepository;
    private readonly IFacebookService _facebookService = facebookService;
    
    public async Task<Result<IEnumerable<FacebookPagePostComment>>> Handle(GetFacebookPagePostCommentsQuery request, CancellationToken cancellationToken)
    {
        var user = await _facebookUserRepository.GetByUserId(request.UserId);
        if (user is null)
        {
            return Result.Fail("User not found");
        }

        var accessTokenResult = await _facebookService.GetPageAccessToken(user, request.PageId);
        if (accessTokenResult.IsFailed)
        {
            return Result.Fail(accessTokenResult.Errors);
        }

        var commentsResult = await _facebookService.GetPostComments(request.PostId, accessTokenResult.Value);
        if (commentsResult.IsFailed)
        {
            return Result.Fail(commentsResult.Errors);
        }

        return Result.Ok(commentsResult.Value);
    }
}