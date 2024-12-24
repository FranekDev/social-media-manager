namespace SocialMediaManager.Application.Features.Facebook.Page.Commands.CreatePagePostComment;

public record CreatePagePostCommentRequest(string PagePostId, string PostCommentId, string Message);
