using FluentValidation;

namespace SocialMediaManager.Application.Features.Instagram.Media.Commands.CreateInstagramReel;

public class CreateInstagramReelCommandValidator : AbstractValidator<CreateInstagramReelRequest>
{
    public CreateInstagramReelCommandValidator()
    {
        RuleFor(p => p.VideoBytes)
            .NotEmpty().WithMessage("VideoUrl is required.");
        
        RuleFor(p => p.Caption)
            .MaximumLength(22000).WithMessage("Caption must not exceed 2200 characters.");
        
        RuleFor(p => p.ScheduledAt)
            .NotEmpty().WithMessage("ScheduledAt is required")
            .GreaterThan(DateTime.UtcNow).WithMessage("ScheduledAt must be in the future");
    }
}