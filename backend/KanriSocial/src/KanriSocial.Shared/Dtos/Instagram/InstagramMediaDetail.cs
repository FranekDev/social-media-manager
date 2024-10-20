using KanriSocial.Shared.Enums.Instagram;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KanriSocial.Shared.Dtos.Instagram;

public record InstagramMediaDetail
{
    public string Caption { get; init; }

    [JsonProperty("comments_count")]
    public int CommentsCount { get; init; }

    public string Id { get; init; }

    [JsonProperty("is_comment_enabled")]
    public bool IsCommentEnabled { get; init; }

    [JsonProperty("is_shared_to_feed")]
    public bool IsSharedToFeed { get; init; }

    [JsonProperty("like_count")]
    public int LikeCount { get; init; }

    [JsonProperty("media_product_type")]
    [JsonConverter(typeof(StringEnumConverter))]
    public InstagramMediaProductType MediaProductType { get; init; }

    [JsonProperty("media_type")]
    [JsonConverter(typeof(StringEnumConverter))]
    public InstagramMediaType MediaType { get; init; }

    [JsonProperty("media_url")]
    public Uri MediaUrl { get; init; }

    public InstagramMediaOwner Owner { get; init; }

    public Uri Permalink { get; init; }

    public string Shortcode { get; init; }

    [JsonProperty("thumbnail_url")]
    public Uri ThumbnailUrl { get; init; }

    public DateTime Timestamp { get; init; }

    public string Username { get; init; }
}

public record InstagramMediaOwner
{
    public string Id { get; init; }
}
