using FluentResults;
using KanriSocial.Shared.Dtos.Instagram;

namespace KanriSocial.Application.Services.Instagram.Interfaces;

public interface IInstagramUserService
{
    Task<Result<InstagramUserDetail>> GetInstagramUserDetailById(Guid userId);
}