﻿namespace KanriSocial.Application.Features.Instagram.Post.Commands.CreateInstagramPost;

public record CreateInstagramPostRequest
{
    public string ImageBytes { get; set; }
    public string Caption { get; set; }
    public DateTime ScheduledAt { get; set; }
}