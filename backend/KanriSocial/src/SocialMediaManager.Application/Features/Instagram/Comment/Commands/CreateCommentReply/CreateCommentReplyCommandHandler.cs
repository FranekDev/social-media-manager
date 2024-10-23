using FluentResults;
using SocialMediaManager.Shared.Dtos.Instagram;
using MediatR;
using SocialMediaManager.Application.Services.Instagram.Interfaces;

namespace SocialMediaManager.Application.Features.Instagram.Comment.Commands.CreateCommentReply;

public class CreateCommentReplyCommandHandler(IInstagramMediaService instagramMediaService) : IRequestHandler<CreateCommentReplyCommand, Result<InstagramCommentReply>> 
{
    private readonly IInstagramMediaService _instagramMediaService = instagramMediaService;
    
    public async Task<Result<InstagramCommentReply>> Handle(CreateCommentReplyCommand request, CancellationToken cancellationToken)
    {
        var result = await _instagramMediaService.ReplyToComment(request.CommentId, request.Message, request.UserId);
        
        return Result.Ok(result.Value);
    }
}