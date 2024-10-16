﻿using KanriSocial.Domain.Models.Instagram.User;

namespace KanriSocial.Infrastructure.Repositories.Instagram.Interfaces.User;

public interface IInstagramUserRepository
{
    Task<Guid> Create(InstagramUser user);
    Task<IEnumerable<InstagramUser>> GetAll();
    Task<InstagramUser?> GetByUserId(Guid userId);
    Task<InstagramUser?> GetByInstagramUserId(Guid instagramUserId);
}