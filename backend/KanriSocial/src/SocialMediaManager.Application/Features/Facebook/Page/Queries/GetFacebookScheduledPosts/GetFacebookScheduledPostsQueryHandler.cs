using MediatR;
using SocialMediaManager.Infrastructure.Repositories.Facebook.Interfaces;
using SocialMediaManager.Shared.Dtos.Facebook;

namespace SocialMediaManager.Application.Features.Facebook.Page.Queries.GetFacebookScheduledPosts;

public class GetFacebookScheduledPostsQueryHandler(IFacebookFeedRepository facebookFeedRepository) : IRequestHandler<GetFacebookScheduledPostsQuery, IEnumerable<FacebookFeedPostDto>>
{
    private readonly IFacebookFeedRepository _facebookFeedRepository = facebookFeedRepository;
    
    public async Task<IEnumerable<FacebookFeedPostDto>> Handle(GetFacebookScheduledPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await _facebookFeedRepository.GetUnpublishedPosts(request.UserId);
        return posts.Select(p => new FacebookFeedPostDto(p.Id, p.PageId, p.Message, p.ScheduledAt));
    }
}