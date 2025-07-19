using System.Text.Json.Serialization;

namespace ScriptBloxApi.Objects;

public record Error(
    [property: JsonPropertyName("message")] string Message
);