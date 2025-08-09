using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

namespace ScriptBloxApi.Objects;

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
public record Game(
    [property: JsonPropertyName("_id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("imageUrl")] string ImageUrl
);

public record Results(
    [property: JsonPropertyName("totalPages")] int? TotalPages,
    [property: JsonPropertyName("nextPage")] int? NextPage,
    [property: JsonPropertyName("max")] int? Max,
    [property: JsonPropertyName("scripts")] IReadOnlyList<Script> Scripts
);

public record FetchResult(
    [property: JsonPropertyName("result")] Results Result
);

public record Script(
    [property: JsonPropertyName("_id")] string Id,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("game")] Game Game,
    [property: JsonPropertyName("slug")] string Slug,
    [property: JsonPropertyName("verified")] bool? Verified,
    [property: JsonPropertyName("key")] bool? Key,
    [property: JsonPropertyName("views")] int? Views,
    [property: JsonPropertyName("scriptType")] string ScriptType,
    [property: JsonPropertyName("isUniversal")] bool? IsUniversal,
    [property: JsonPropertyName("isPatched")] bool? IsPatched,
    [property: JsonPropertyName("image")] string Image,
    [property: JsonPropertyName("createdAt")] DateTime? CreatedAt,
    [property: JsonPropertyName("script")] string? ScriptData
);

