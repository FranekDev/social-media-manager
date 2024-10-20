﻿using FluentResults;
using MediatR;

namespace KanriSocial.Application.Features.Instagram.Post.Commands.CreateInstagramPost;

public record CreateInstagramPostCommand(string ImageBytes, string Caption, DateTime ScheduledAt, Guid UserId) : IRequest<Result<Guid>>;
