using KanriSocial.Domain.Models;

namespace KanriSocial.Application.Dtos.Instagram;

public class InstagramUserDto
{
    public Guid Id { get; init; }
    public string AccountId { get; init; }
    public User User { get; init; }
}