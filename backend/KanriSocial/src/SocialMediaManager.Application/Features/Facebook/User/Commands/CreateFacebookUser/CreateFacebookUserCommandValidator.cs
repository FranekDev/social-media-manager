using FluentValidation;

namespace SocialMediaManager.Application.Features.Facebook.User.Commands.CreateFacebookUser;

public class CreateFacebookUserCommandValidator : AbstractValidator<CreateFacebookUserCommand>
{
    public CreateFacebookUserCommandValidator()
    {
        RuleFor(x => x.FacebookUserId)
            .NotEmpty().WithMessage("FacebookUserId is required");

        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required");
    }
}