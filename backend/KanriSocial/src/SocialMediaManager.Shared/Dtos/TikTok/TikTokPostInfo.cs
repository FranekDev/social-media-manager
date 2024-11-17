using Newtonsoft.Json;

namespace SocialMediaManager.Shared.Dtos.TikTok;

public record TikTokPostInfo(
    string Title,
    [JsonProperty("privacy_level")] string PrivacyLevel,
    [JsonProperty("disable_duet")] bool DisableDuet,
    [JsonProperty("disable_comment")] bool DisableComment,
    [JsonProperty("disable_stitch")] bool DisableStitch,
    [JsonProperty("video_cover_timestamp_ms")] long VideoCoverTimestampMs
);
