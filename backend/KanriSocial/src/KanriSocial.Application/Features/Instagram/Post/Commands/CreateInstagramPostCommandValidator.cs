using FluentValidation;

namespace KanriSocial.Application.Features.Instagram.Post.Commands;

public sealed class CreateInstagramPostCommandValidator : AbstractValidator<CreateInstagramPostCommand>
{
    public CreateInstagramPostCommandValidator()
    {
        RuleFor(x => x.Caption)
            .MaximumLength(2200).WithMessage("Caption must not exceed 2200 characters");

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("ImageUrl is required");

        RuleFor(x => x.ScheduledAt)
            .NotEmpty().WithMessage("ScheduledAt is required")
            .GreaterThan(DateTime.UtcNow).WithMessage("ScheduledAt must be in the future");
        
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required");
    }
}