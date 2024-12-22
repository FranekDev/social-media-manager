using Newtonsoft.Json;

namespace SocialMediaManager.Shared.Dtos.Facebook;

public record FacebookPagePicture(
    short Height,
    [JsonProperty("is_silhouette")] bool IsSilhouette,
    string Url,
    short Width
);