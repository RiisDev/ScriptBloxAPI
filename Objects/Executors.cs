using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ScriptBloxApi.Objects;

public record Executor(
    [property: JsonPropertyName("_id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("patched")] bool? Patched,
    [property: JsonPropertyName("platform")] string Platform,
    [property: JsonPropertyName("website")] string Website,
    [property: JsonPropertyName("discord")] string Discord,
    [property: JsonPropertyName("version")] string Version,
    [property: JsonPropertyName("versionDate")] DateTime? VersionDate,
    [property: JsonPropertyName("thumbnail")] string Thumbnail,
    [property: JsonPropertyName("store")] string Store,
    [property: JsonPropertyName("type")] string Type
);

public record ExecutorInfo(
    [property: JsonPropertyName("_id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("website")] string Website,
    [property: JsonPropertyName("discord")] string Discord,
    [property: JsonPropertyName("developers")] string Developers,
    [property: JsonPropertyName("images")] IReadOnlyList<string> Images,
    [property: JsonPropertyName("showcase")] string Showcase,
    [property: JsonPropertyName("thumbnail")] string Thumbnail,
    [property: JsonPropertyName("store")] string Store
);