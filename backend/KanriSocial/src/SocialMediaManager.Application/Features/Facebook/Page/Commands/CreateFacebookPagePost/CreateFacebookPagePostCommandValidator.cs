using FluentValidation;

namespace SocialMediaManager.Application.Features.Facebook.Page.Commands.CreateFacebookPagePost;

public class CreateFacebookPagePostCommandValidator : AbstractValidator<CreateFacebookPagePostCommand>
{
    public CreateFacebookPagePostCommandValidator()
    {
        RuleFor(x => x.PageId)
            .NotEmpty().WithMessage("PageId is required");
        
        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Message is required");
        
        RuleFor(x => x.ScheduledAt)
            .NotEmpty().WithMessage("ScheduledAt is required")
            .GreaterThan(DateTime.UtcNow).WithMessage("ScheduledAt must be in the future");
    }
}