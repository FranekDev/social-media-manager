using FluentValidation;

namespace SocialMediaManager.Application.Features.TikTok.Commands.CreateTikTokUser;

public class CreateTikTokUserCommandValidator : AbstractValidator<CreateTikTokUserCommand>
{
    public CreateTikTokUserCommandValidator()
    {
        RuleFor(p => p.Code)
            .NotEmpty().WithMessage("Code is required.");
    }
}