using FluentResults;
using MediatR;
using SocialMediaManager.Domain.Dtos.Instagram;
using SocialMediaManager.Infrastructure.Repositories.Instagram.Interfaces;
using SocialMediaManager.Infrastructure.Repositories.Instagram.Interfaces.User;

namespace SocialMediaManager.Application.Features.Instagram.Media.Queries.GetUnpublishedPosts;

public class GetUnpublishedPostsQueryHandler(
    IInstagramUserRepository userRepository, 
    IInstagramPostRepository instagramPostRepository) : IRequestHandler<GetUnpublishedPostsQuery, Result<IEnumerable<InstagramPostDto>>>
{
    private readonly IInstagramUserRepository _userRepository = userRepository;
    private readonly IInstagramPostRepository _instagramPostRepository = instagramPostRepository;
    
    public async Task<Result<IEnumerable<InstagramPostDto>>> Handle(GetUnpublishedPostsQuery request, CancellationToken cancellationToken)
    {
        var instagramUser = await _userRepository.GetByUserId(request.UserId);
        
        if (instagramUser == null)
        {
            return Result.Fail("User not found");
        }
        
        var posts = await _instagramPostRepository.GetUnpublishedPosts(request.UserId);
        var result = posts.Select(p => new InstagramPostDto(
            p.Id,
            p.ImageUrl,
            p.Caption,
            p.ScheduledAt,
            request.UserId,
            p.IsPublished
        ));
        
        return Result.Ok(result);
    }
}