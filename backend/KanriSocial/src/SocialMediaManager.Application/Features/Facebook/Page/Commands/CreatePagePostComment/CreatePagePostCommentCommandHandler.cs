using FluentResults;
using MediatR;
using SocialMediaManager.Application.Services.Facebook.Interfaces;
using SocialMediaManager.Infrastructure.Clients.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.Facebook.Interfaces;
using SocialMediaManager.Shared.Dtos.Facebook;

namespace SocialMediaManager.Application.Features.Facebook.Page.Commands.CreatePagePostComment;

public class CreatePagePostCommentCommandHandler(
    IFacebookClient facebookClient, 
    IFacebookUserRepository facebookUserRepository,
    IFacebookService facebookService) : IRequestHandler<CreatePagePostCommentCommand, Result<FacebookNewComment>>
{
    private readonly IFacebookClient _facebookClient = facebookClient;
    private readonly IFacebookUserRepository _facebookUserRepository = facebookUserRepository;
    private readonly IFacebookService _facebookService = facebookService;

    public async Task<Result<FacebookNewComment>> Handle(CreatePagePostCommentCommand request, CancellationToken cancellationToken)
    {
        var fbUser = await _facebookUserRepository.GetByUserId(request.UserId);
        if (fbUser is null)
        {
            return Result.Fail("Nie znaleziono użytkownika");
        }
        
        var pageId = request.PagePostId.Split("_")[0];
        var pageDataResult = await _facebookService.GetPageData(fbUser, pageId);
        if (pageDataResult.IsFailed)
        {
            return Result.Fail(pageDataResult.Errors);
        }
        
        var accountData = await _facebookService.GetAccountData(fbUser);
        if (accountData.IsFailed)
        {
            return Result.Fail(accountData.Errors);
        }
        
        var accessTokenResult = await _facebookService.GetPageAccessToken(fbUser, pageId);
        if (accessTokenResult.IsFailed)
        {
            return Result.Fail(accessTokenResult.Errors);
        }
        
        var commentResult = await _facebookClient.CreatePagePostComment(request.PostCommentId, request.Message, accessTokenResult.Value);
        if (commentResult.IsFailed)
        {
            return Result.Fail<FacebookNewComment>(commentResult.Errors);
        }
        
        if (commentResult.Value is null)
        {
            return Result.Fail("Nie udało się dodać komentarza");
        }
        
        return commentResult.Value;
    }
}