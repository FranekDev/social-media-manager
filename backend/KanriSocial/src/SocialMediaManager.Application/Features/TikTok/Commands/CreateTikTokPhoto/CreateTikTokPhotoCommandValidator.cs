using FluentValidation;

namespace SocialMediaManager.Application.Features.TikTok.Commands.CreateTikTokPhoto;

public class CreateTikTokPhotoCommandValidator : AbstractValidator<CreateTikTokPhotoCommand>
{
    public CreateTikTokPhotoCommandValidator()
    {
        var polishTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");

        RuleFor(p => p.Title)
        .NotEmpty().WithMessage("Tytuł jest wymagany.")
        .MaximumLength(90).WithMessage("Tytuł nie może przekraczać 90 znaków.");

        RuleFor(p => p.Description)
        .NotEmpty().WithMessage("Opis jest wymagany.")
        .MaximumLength(4000).WithMessage("Opis nie może przekraczać 4000 znaków.");

        RuleFor(p => p.ImagesBytes)
        .NotEmpty().WithMessage("Zdjęcia są wymagane.")
        .Must(images => images is { Count: > 0 })
        .WithMessage("Zdjęcia muszą zawierać co najmniej jeden element.");

        RuleFor(p => p.ScheduledAt)
        .NotEmpty().WithMessage("Data zaplanowania jest wymagana.")
        .GreaterThan(DateTime.UtcNow).WithMessage(p =>
        {
            var comparisonValueInPolishTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, polishTimeZone);
            return $"Data zaplanowania musi być większa niż {comparisonValueInPolishTime}.";
        });
    }
}