using System;
using System.Text.Json.Serialization;

namespace ScriptBloxApi.Objects;
public record Versions(
    [property: JsonPropertyName("_id")] string Id,
    [property: JsonPropertyName("version")] string Version,
    [property: JsonPropertyName("platform")] string Platform,
    [property: JsonPropertyName("versionDate")] DateTime? VersionDate,
    [property: JsonPropertyName("updatedAt")] DateTime? UpdatedAt,
    [property: JsonPropertyName("__v")] int? V
);