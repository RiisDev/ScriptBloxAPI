using System;
using System.Text.Json.Serialization;

namespace ScriptBloxApi.Objects;

public record Discord(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("discriminator")] string Discriminator
);

public record UserInfo(
    [property: JsonPropertyName("user")] User User
);

public record User(
    [property: JsonPropertyName("_id")] string _Id,
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("verified")] bool? Verified,
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("profilePicture")] string ProfilePicture,
    [property: JsonPropertyName("bio")] string Bio,
    [property: JsonPropertyName("createdAt")] DateTime? CreatedAt,
    [property: JsonPropertyName("lastActive")] DateTime? LastActive,
    [property: JsonPropertyName("discord")] Discord Discord,
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("followingCount")] int? FollowingCount,
    [property: JsonPropertyName("followersCount")] int? FollowersCount
);