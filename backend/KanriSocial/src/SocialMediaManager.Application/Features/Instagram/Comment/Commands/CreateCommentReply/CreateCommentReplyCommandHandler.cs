using FluentResults;
using SocialMediaManager.Shared.Dtos.Instagram;
using MediatR;
using SocialMediaManager.Application.Services.Instagram.Interfaces;

namespace SocialMediaManager.Application.Features.Instagram.Comment.Commands.CreateCommentReply;

public class CreateCommentReplyCommandHandler(IInstagramPostService instagramPostService) : IRequestHandler<CreateCommentReplyCommand, Result<InstagramCommentReply>> 
{
    private readonly IInstagramPostService _instagramPostService = instagramPostService;
    
    public async Task<Result<InstagramCommentReply>> Handle(CreateCommentReplyCommand request, CancellationToken cancellationToken)
    {
        var result = await _instagramPostService.ReplyToComment(request.CommentId, request.Message, request.UserId);
        
        return Result.Ok(result.Value);
    }
}