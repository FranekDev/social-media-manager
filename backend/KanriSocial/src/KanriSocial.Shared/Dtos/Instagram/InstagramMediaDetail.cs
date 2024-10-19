using Newtonsoft.Json;

namespace KanriSocial.Shared.Dtos.Instagram;

public record InstagramMediaDetail
{
    [JsonProperty("caption")]
    public string Caption { get; init; }

    [JsonProperty("comments_count")]
    public int CommentsCount { get; init; }

    [JsonProperty("copyright_check_information")]
    public CopyrightCheckInformation CopyrightCheckInformation { get; init; }

    [JsonProperty("id")]
    public string Id { get; init; }

    [JsonProperty("is_comment_enabled")]
    public bool IsCommentEnabled { get; init; }

    [JsonProperty("is_shared_to_feed")]
    public bool IsSharedToFeed { get; init; }

    [JsonProperty("like_count")]
    public int LikeCount { get; init; }

    [JsonProperty("media_product_type")]
    public string MediaProductType { get; init; }

    [JsonProperty("media_type")]
    public string MediaType { get; init; }

    [JsonProperty("media_url")]
    public Uri MediaUrl { get; init; }

    [JsonProperty("owner")]
    public string Owner { get; init; }

    [JsonProperty("permalink")]
    public Uri Permalink { get; init; }

    [JsonProperty("shortcode")]
    public string Shortcode { get; init; }

    [JsonProperty("thumbnail_url")]
    public Uri ThumbnailUrl { get; init; }

    [JsonProperty("timestamp")]
    public DateTime Timestamp { get; init; }

    [JsonProperty("username")]
    public string Username { get; init; }
}

public record CopyrightCheckInformation
{
    [JsonProperty("status")]
    public string Status { get; init; }

    [JsonProperty("matches_found")]
    public bool MatchesFound { get; init; }

    [JsonProperty("copyright_matches")]
    public List<CopyrightMatch> CopyrightMatches { get; init; }
}

public record CopyrightMatch
{
    [JsonProperty("author")]
    public string Author { get; init; }

    [JsonProperty("content_title")]
    public string ContentTitle { get; init; }

    [JsonProperty("matched_segments")]
    public List<MatchedSegment> MatchedSegments { get; init; }

    [JsonProperty("owner_copyright_policy")]
    public OwnerCopyrightPolicy OwnerCopyrightPolicy { get; init; }
}

public record MatchedSegment
{
    [JsonProperty("duration_in_seconds")]
    public int DurationInSeconds { get; init; }

    [JsonProperty("segment_type")]
    public string SegmentType { get; init; }

    [JsonProperty("start_time_in_seconds")]
    public int StartTimeInSeconds { get; init; }
}

public record OwnerCopyrightPolicy
{
    [JsonProperty("name")]
    public string Name { get; init; }

    [JsonProperty("actions")]
    public List<PolicyAction> Actions { get; init; }
}

public record PolicyAction
{
    [JsonProperty("action")]
    public string Action { get; init; }

    [JsonProperty("geos")]
    public List<string> Geos { get; init; }
}