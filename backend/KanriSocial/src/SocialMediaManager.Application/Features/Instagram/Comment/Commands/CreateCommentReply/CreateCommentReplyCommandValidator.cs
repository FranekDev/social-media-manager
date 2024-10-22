using FluentValidation;

namespace SocialMediaManager.Application.Features.Instagram.Comment.Commands.CreateCommentReply;

public class CreateCommentReplyCommandValidator : AbstractValidator<CreateCommentReplyCommand>
{
    public CreateCommentReplyCommandValidator()
    {
        RuleFor(p => p.Message)
            .NotEmpty().WithMessage("Message is required.")
            .NotNull()
            .MaximumLength(22000).WithMessage("Message must not exceed 2200 characters.");
    }
}