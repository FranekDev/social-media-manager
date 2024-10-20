using FluentResults;
using KanriSocial.Application.Services.Instagram.Interfaces;
using KanriSocial.Shared.Dtos.Instagram;
using MediatR;

namespace KanriSocial.Application.Features.Instagram.User.Queries.GetInstagramUser;

public class GetInstagramUserByUserIdQueryHandler(IInstagramUserService instagramUserService) : IRequestHandler<GetInstagramUserByUserIdQuery, Result<InstagramUserDetail>>
{
    private readonly IInstagramUserService _instagramUserService = instagramUserService;
    
    public async Task<Result<InstagramUserDetail>> Handle(GetInstagramUserByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _instagramUserService.GetInstagramUserDetailById(request.UserId);
    }
}