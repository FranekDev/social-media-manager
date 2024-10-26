using FluentResults;
using MediatR;
using SocialMediaManager.Application.Services.Facebook.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.Facebook.Interfaces;
using SocialMediaManager.Shared.Dtos.Facebook;

namespace SocialMediaManager.Application.Features.Facebook.Page.Commands.CreateFacebookPagePost;

public class CreateFacebookPagePostCommandHandler(
    IFacebookUserRepository facebookUserRepository,
    IFacebookService facebookService) : IRequestHandler<CreateFacebookPagePostCommand, Result<Guid?>>
{
    private readonly IFacebookUserRepository _facebookUserRepository = facebookUserRepository;
    private readonly IFacebookService _facebookService = facebookService;
    
    public async Task<Result<Guid?>> Handle(CreateFacebookPagePostCommand request, CancellationToken cancellationToken)
    {
        var user = await _facebookUserRepository.GetByUserId(request.UserId);
        if (user is null)
        {
            return Result.Fail("User not found");
        }

        var pageDataResult = await _facebookService.GetPageData(user, request.PageId);
        if (pageDataResult.IsFailed)
        {
            return Result.Fail(pageDataResult.Errors);
        }

        var accountData = await _facebookService.GetAccountData(user);
        if (accountData.IsFailed)
        {
            return Result.Fail(accountData.Errors);
        }
        
        var pageAccessToken = accountData.Value.FirstOrDefault(x => x.Id == request.PageId)?.AccessToken;
        if (pageAccessToken is null)
        {
            return Result.Fail("Page access token not found");
        }

        
        var feedPost = new FacebookFeedPostDto (null, request.PageId, request.Message, request.ScheduledAt);
        var pagePostResult = await _facebookService.SchedulePagePost(feedPost, user, pageAccessToken);
        
        if (pagePostResult.IsFailed)
        {
            return Result.Fail(pagePostResult.Errors);
        }
        
        return pagePostResult.Value;
    }
}