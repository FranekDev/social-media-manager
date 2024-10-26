using FluentResults;
using MediatR;
using SocialMediaManager.Shared.Dtos.Facebook;

namespace SocialMediaManager.Application.Features.Facebook.Page.Queries.GetFacebookPageData;

public record GetFacebookPageDataQuery(string PageId, Guid UserId) : IRequest<Result<FacebookPageData?>>;