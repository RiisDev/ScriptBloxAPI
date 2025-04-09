using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;

namespace ScriptBloxApi.Objects;
public record Comment(
    [property: JsonPropertyName("_id")] string Id,
    [property: JsonPropertyName("commentBy")] CommentBy CommentBy,
    [property: JsonPropertyName("text")] string Text,
    [property: JsonPropertyName("createdAt")] DateTime? CreatedAt,
    [property: JsonPropertyName("__v")] int? V,
    [property: JsonPropertyName("edited")] bool? Edited,
    [property: JsonPropertyName("likeCount")] int? LikeCount,
    [property: JsonPropertyName("dislikeCount")] int? DislikeCount,
    [property: JsonPropertyName("liked")] bool? Liked,
    [property: JsonPropertyName("disliked")] bool? Disliked
);

public record CommentBy(
    [property: JsonPropertyName("_id")] string _Id,
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("verified")] bool? Verified,
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("profilePicture")] string ProfilePicture,
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("id")] string Id
);

public record CommentResult(
    [property: JsonPropertyName("totalPages")] int? TotalPages,
    [property: JsonPropertyName("comments")] IReadOnlyList<Comment> Comments
);

public record CommentRoot(
    [property: JsonPropertyName("result")] CommentResult Result
);

