﻿namespace KanriSocial.Application.Services.Instagram.Interfaces;

public interface IInstagramService
{
    Task<string?> GetLongLivedToken(string accessToken);
}