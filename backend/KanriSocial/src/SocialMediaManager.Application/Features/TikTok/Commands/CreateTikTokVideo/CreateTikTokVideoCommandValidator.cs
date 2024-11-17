using FluentValidation;

namespace SocialMediaManager.Application.Features.TikTok.Commands.CreateTikTokVideo;

public class CreateTikTokVideoCommandValidator : AbstractValidator<CreateTikTokVideoCommand>
{
    public CreateTikTokVideoCommandValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty().WithMessage("Title is required.");
        
        RuleFor(p => p.VideoBytes)
            .NotEmpty().WithMessage("VideoBytes is required.");
    }
}