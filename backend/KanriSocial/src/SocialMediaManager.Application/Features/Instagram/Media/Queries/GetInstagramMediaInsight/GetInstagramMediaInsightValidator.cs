using FluentValidation;

namespace SocialMediaManager.Application.Features.Instagram.Media.Queries.GetInstagramMediaInsight;

public class GetInstagramMediaInsightValidator : AbstractValidator<GetInstagramMediaInsightQuery>
{
    public GetInstagramMediaInsightValidator()
    {
        RuleFor(x => x.MediaId)
            .NotEmpty().WithMessage("MediaId is required.");

        RuleFor(x => x.InsightType)
            .IsInEnum().WithMessage("InsightType is not valid.");
    }
}