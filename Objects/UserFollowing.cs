using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ScriptBloxApi.Objects;

public record FollowRoot(
    [property: JsonPropertyName("users")] IReadOnlyList<FollowUser> Users,
    [property: JsonPropertyName("totalPages")] int? TotalPages
);

public record FollowUser(
    [property: JsonPropertyName("_id")] string Id,
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("profilePicture")] string ProfilePicture
);