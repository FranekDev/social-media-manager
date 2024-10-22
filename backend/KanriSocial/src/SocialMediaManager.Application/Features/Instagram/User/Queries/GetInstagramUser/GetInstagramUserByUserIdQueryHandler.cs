using FluentResults;
using SocialMediaManager.Shared.Dtos.Instagram;
using MediatR;
using SocialMediaManager.Application.Services.Instagram.Interfaces;

namespace SocialMediaManager.Application.Features.Instagram.User.Queries.GetInstagramUser;

public class GetInstagramUserByUserIdQueryHandler(IInstagramUserService instagramUserService) : IRequestHandler<GetInstagramUserByUserIdQuery, Result<InstagramUserDetail>>
{
    private readonly IInstagramUserService _instagramUserService = instagramUserService;
    
    public async Task<Result<InstagramUserDetail>> Handle(GetInstagramUserByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _instagramUserService.GetInstagramUserDetailById(request.UserId);
    }
}