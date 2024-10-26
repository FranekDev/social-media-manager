using FluentResults;
using MediatR;
using SocialMediaManager.Application.Services.Facebook.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.Facebook.Interfaces;
using SocialMediaManager.Shared.Dtos.Facebook;

namespace SocialMediaManager.Application.Features.Facebook.Page.Queries.GetFacebookPageData;

public class GetFacebookPageDataQueryHandler(
    IFacebookService facebookService,
    IFacebookUserRepository facebookUserRepository) : IRequestHandler<GetFacebookPageDataQuery, Result<FacebookPageData?>>
{
    private readonly IFacebookService _facebookService = facebookService;
    private readonly IFacebookUserRepository _facebookUserRepository = facebookUserRepository;
    
    public async Task<Result<FacebookPageData?>> Handle(GetFacebookPageDataQuery request, CancellationToken cancellationToken)
    {
        var facebookUser = await _facebookUserRepository.GetByUserId(request.UserId);

        if (facebookUser is null)
        {
            return Result.Fail("Facebook user not found.");
        }
        
        var pageDataResult = await _facebookService.GetPageData(facebookUser, request.PageId);
        if (pageDataResult.IsFailed)
        {
            return Result.Fail(pageDataResult.Errors);
        }
        
        return pageDataResult?.Value;
    }
}