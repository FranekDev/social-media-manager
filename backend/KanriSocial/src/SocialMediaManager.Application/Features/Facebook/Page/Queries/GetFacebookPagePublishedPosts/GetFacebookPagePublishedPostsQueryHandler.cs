using FluentResults;
using MediatR;
using SocialMediaManager.Application.Services.Facebook.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.Facebook.Interfaces;
using SocialMediaManager.Shared.Dtos.Facebook;

namespace SocialMediaManager.Application.Features.Facebook.Page.Queries.GetFacebookPagePublishedPosts;

public class GetFacebookPagePublishedPostsQueryHandler(
    IFacebookService facebookService,
    IFacebookUserRepository facebookUserRepository) : IRequestHandler<GetFacebookPagePublishedPostsQuery, Result<IEnumerable<FacebookPublishedPost>>>
{
    private readonly IFacebookService _facebookService = facebookService;
    private readonly IFacebookUserRepository _facebookUserRepository = facebookUserRepository;
    
    public async Task<Result<IEnumerable<FacebookPublishedPost>>> Handle(GetFacebookPagePublishedPostsQuery request, CancellationToken cancellationToken)
    {
        var user = await _facebookUserRepository.GetByUserId(request.UserId);
        if (user is null)
        {
            return Result.Fail("User not found");
        }
        
        var pageAccessToken = await _facebookService.GetPageAccessToken(user, request.PageId);
        if (pageAccessToken.IsFailed)
        {
            return Result.Fail(pageAccessToken.Errors);
        }
        
        var publishedPosts = await _facebookService.GetPublishedPosts(request.PageId, pageAccessToken.Value);
        if (publishedPosts.IsFailed)
        {
            return Result.Fail(publishedPosts.Errors);
        }
        
        return Result.Ok(publishedPosts.Value);
    }
}