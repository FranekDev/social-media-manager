using FluentResults;
using MediatR;
using SocialMediaManager.Application.Services.Facebook.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.Facebook.Interfaces;
using SocialMediaManager.Shared.Dtos.Facebook;

namespace SocialMediaManager.Application.Features.Facebook.Page.Queries.GetFacebookUserPages;

public class GetFacebookUserPagesQueryHandler(
    IFacebookUserRepository facebookUserRepository,
    IFacebookService facebookService) : IRequestHandler<GetFacebookUserPagesQuery, Result<IEnumerable<FacebookUserPage>>>
{
    private readonly IFacebookUserRepository _facebookUserRepository = facebookUserRepository;
    private readonly IFacebookService _facebookService = facebookService;
    
    
    public async Task<Result<IEnumerable<FacebookUserPage>>> Handle(GetFacebookUserPagesQuery request, CancellationToken cancellationToken)
    {
        var fbUser = await _facebookUserRepository.GetByUserId(request.UserId);

        if (fbUser is null)
        {
            Result.Fail("Nie znaleziono konta Facebook");
        }

        var pages = await _facebookService.GetUserPages(fbUser!);
        return pages.IsFailed ? Result.Fail(pages.Errors) : Result.Ok(pages.Value);
    }
}