using FluentValidation;

namespace SocialMediaManager.Application.Features.TikTok.Commands.CreateTikTokPhoto;

public class CreateTikTokPhotoCommandValidator : AbstractValidator<CreateTikTokPhotoCommand>
{
    public CreateTikTokPhotoCommandValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(90).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(p => p.Descriptiton)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(4000).WithMessage("{PropertyName} must not exceed 500 characters.");

        RuleFor(p => p.ImagesBytes)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(p => p.ScheduledAt)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .GreaterThan(DateTime.UtcNow).WithMessage("{PropertyName} must be greater than {ComparisonValue}.");
    }
}