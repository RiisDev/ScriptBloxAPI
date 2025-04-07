# ScriptBlox API
[![Support Server](https://img.shields.io/discord/477201632204161025.svg?label=Discord&logo=Discord&colorB=7289da&style=for-the-badge)](https://discord.gg/yyuggrH) ![GitHub](https://img.shields.io/github/license/RiisDev/ScriptBloxAPI?style=for-the-badge) ![Nuget All Releases](https://img.shields.io/nuget/dt/ScriptBloxAPI?label=Nuget%20Downloads&style=for-the-badge) ![](https://img.shields.io/badge/.NET-Standard%202.0-blueviolet?style=for-the-badge)

## License

[MIT](https://choosealicense.com/licenses/mit)

## Documentation

Follows [ScriptBloxAPI Standards and Parameters](https://scriptblox.com/docs/scripts/fetch)
Sure! Here's a `README.md` tailored for the `Scripts` class in your `ScriptBloxApi` C# library. It includes method descriptions, usage examples, and parameter details.

---

# ScriptBloxApi - Scripts Module

The `Scripts` class in the `ScriptBloxApi` library provides a set of asynchronous methods to interact with the ScriptBlox API for fetching, searching, and retrieving Roblox scripts.

## ✨ Features

- Fetch paginated script lists with filters
- Retrieve specific script data
- Get raw script code
- Access trending scripts
- Perform advanced search queries

---

## 🚀 Usage

Make sure to call these methods from an async context.

```csharp
using ScriptBloxApi.Scripts;
using ScriptBloxApi.Objects;

Results scripts = await Scripts.FetchScriptsAsync();
ScriptData script = await Scripts.FetchScriptAsync("abc123");
string rawScript = await Scripts.FetchRawScriptAsync("abc123");
IReadOnlyList<Script> trending = await Scripts.FetchTrendingScriptsAsync();
IReadOnlyList<Script> searchResults = await Scripts.SearchScriptsAsync("infinite yield");
```

---

## 📚 API Reference

### `FetchScriptsAsync(...)`
Fetches a paginated list of scripts with optional filtering and sorting.

**Parameters:**

| Name        | Type             | Description                                  |
|-------------|------------------|----------------------------------------------|
| `page`      | `int?`           | Page number (default: 1)                     |
| `max`       | `int?`           | Max results per page (1–20, default: 20)    |
| `mode`      | `ScriptCost?`    | Filter by script cost (`free`, `paid`)      |
| `patched`   | `bool?`          | Include only patched scripts if `true`      |
| `key`       | `bool?`          | Include only key-protected scripts if `true`|
| `universal` | `bool?`          | Filter universal scripts                     |
| `verified`  | `bool?`          | Include only verified scripts                |
| `sortBy`    | `SortBy?`        | Sort field (`views`, `likeCount`, etc.)     |
| `order`     | `Order?`         | Sort order (`asc`, `desc`)                  |

**Returns:** `Task<Results>`

---

### `FetchScriptAsync(string scriptId)`
Fetches metadata for a single script by ID.

**Parameters:**
- `scriptId`: The ID of the script to fetch

**Returns:** `Task<ScriptData>`

---

### `FetchRawScriptAsync(string scriptId)`
Fetches the raw Lua source code for a script by ID.

**Parameters:**
- `scriptId`: The ID of the script

**Returns:** `Task<string>`

---

### `FetchTrendingScriptsAsync(int? max = 20)`
Gets trending scripts, optionally limited to a maximum number.

**Parameters:**
- `max`: Maximum number of scripts (1–20, default: 20)

**Returns:** `Task<IReadOnlyList<Script>>`

---

### `SearchScriptsAsync(...)`
Performs an advanced search for scripts based on a query and filters.

**Parameters:**

| Name        | Type             | Description                                  |
|-------------|------------------|----------------------------------------------|
| `query`     | `string`         | The search query                             |
| `page`      | `int?`           | Page number (default: 1)                     |
| `max`       | `int?`           | Max results per page (1–20, default: 20)     |
| `mode`      | `ScriptCost?`    | Filter by script cost (`free`, `paid`)       |
| `patched`   | `bool?`          | Filter by patched state                      |
| `key`       | `bool?`          | Filter by key-protection                     |
| `universal` | `bool?`          | Filter by universal scripts                  |
| `verified`  | `bool?`          | Filter by verified scripts                   |
| `sortBy`    | `SortBy?`        | Sort field                                   |
| `order`     | `Order?`         | Sort order                                   |
| `strict`    | `bool?`          | Use strict match if `true`                   |

**Returns:** `Task<Results>`

---

## 📌 Enums

### `ScriptCost`
- `free`
- `paid`

### `SortBy`
- `views`
- `likeCount`
- `createdAt`
- `updatedAt`
- `dislikeCount`

### `Order`
- `asc`
- `desc`

---

## 🧪 Example: Search for Free, Verified Scripts

```csharp
var results = await Scripts.SearchScriptsAsync(
    query: "admin",
    mode: Scripts.ScriptCost.free,
    verified: true,
    sortBy: Scripts.SortBy.views,
    order: Scripts.Order.desc
);
```
