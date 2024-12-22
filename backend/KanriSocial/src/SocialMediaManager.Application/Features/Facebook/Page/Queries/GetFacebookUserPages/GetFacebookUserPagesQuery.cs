using FluentResults;
using MediatR;
using SocialMediaManager.Shared.Dtos.Facebook;

namespace SocialMediaManager.Application.Features.Facebook.Page.Queries.GetFacebookUserPages;

public record GetFacebookUserPagesQuery(Guid UserId) : IRequest<Result<IEnumerable<FacebookUserPage>>>;