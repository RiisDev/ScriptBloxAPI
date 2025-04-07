using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ScriptBloxApi.Objects;

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
public record ScriptEditBy(
    [property: JsonPropertyName("_id")] string _Id,
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("id")] string Id
);

public record ScriptGame(
    [property: JsonPropertyName("_id")] string Id,
    [property: JsonPropertyName("gameId")] long? GameId,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("imageUrl")] string ImageUrl
);

public record ScriptOwner(
    [property: JsonPropertyName("_id")] string _Id,
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("verified")] bool? Verified,
    [property: JsonPropertyName("profilePicture")] string ProfilePicture,
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("id")] string Id
);

public record ScriptData(
    [property: JsonPropertyName("script")] ScriptScript Script
);

public record ScriptScript(
    [property: JsonPropertyName("_id")] string Id,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("game")] ScriptGame Game,
    [property: JsonPropertyName("features")] string Features,
    [property: JsonPropertyName("tags")] IReadOnlyList<string> Tags,
    [property: JsonPropertyName("script")] string Script,
    [property: JsonPropertyName("owner")] ScriptOwner Owner,
    [property: JsonPropertyName("slug")] string Slug,
    [property: JsonPropertyName("verified")] bool? Verified,
    [property: JsonPropertyName("views")] int? Views,
    [property: JsonPropertyName("scriptType")] string ScriptType,
    [property: JsonPropertyName("isUniversal")] bool? IsUniversal,
    [property: JsonPropertyName("isPatched")] bool? IsPatched,
    [property: JsonPropertyName("visibility")] string Visibility,
    [property: JsonPropertyName("image")] string Image,
    [property: JsonPropertyName("likeCount")] int? LikeCount,
    [property: JsonPropertyName("dislikeCount")] int? DislikeCount,
    [property: JsonPropertyName("createdAt")] DateTime? CreatedAt,
    [property: JsonPropertyName("editBy")] ScriptEditBy EditBy,
    [property: JsonPropertyName("isFav")] bool? IsFav,
    [property: JsonPropertyName("liked")] bool? Liked,
    [property: JsonPropertyName("disliked")] bool? Disliked
);

