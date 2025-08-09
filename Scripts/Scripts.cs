using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using ScriptBloxApi.LanguageFeatures;
using ScriptBloxApi.Objects;
using static System.Int32;

namespace ScriptBloxApi.Scripts
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class Scripts
    {
        public enum ScriptCost
        {
            free,
            paid
        }

        public enum SortBy
        {
            views,
            likeCount,
            createdAt,
            updatedAt,
            dislikeCount
        }

        public enum Order
        {
            asc,
            desc
        }

        public static async Task<Results> FetchScriptsAsync(
            int? page = 1,
            int? max = 20,
            ScriptCost? mode = null,
            bool? patched = null,
            bool? key = null,
            bool? universal = null,
            bool? verified = null,
            SortBy? sortBy = null,
            Order? order = null
        )
        {
            (string Key, string Value)[] queryParams =
            [
                ("page", page?.ToString()),
                ("max", max.InternalClamp(1, 20).ToString()),
                ("mode", mode?.ToString()),
                ("patched", patched.GetBoolInt()),
                ("key", key.GetBoolInt()),
                ("universal", universal.GetBoolInt()),
                ("verified", verified.GetBoolInt()),
                ("sortBy", sortBy?.ToString()),
                ("order", order?.ToString())
            ];

            FetchResult fetchResult = await Client.Get<FetchResult>("script/fetch", queryParams);

            return fetchResult.Result;
        }

        public static async Task<ScriptData> FetchScriptAsync(string scriptId) => await Client.Get<ScriptData>($"script/{scriptId}", []);

        public static async Task<string> FetchRawScriptAsync(string scriptId) => await Client.Get<string>($"script/raw/{scriptId}", []);

        public static async Task<IReadOnlyList<Script>> FetchTrendingScriptsAsync(int? max = 20)
        {
            FetchResult fetchResult = await Client.Get<FetchResult>("script/trending", [("max", max.InternalClamp(1, 20).ToString())]);
            return fetchResult.Result.Scripts;
        }

        public static async Task<IReadOnlyList<Script>> FetchScriptsFromUser(string username, int? page = 1, int? max = 20)
        {
            FetchResult fetchResult = await Client.Get<FetchResult>($"user/scripts/{username}", [
                ("page", page.InternalClamp(1, MaxValue).ToString()),
                ("max", max.InternalClamp(1, 20).ToString())
            ]);
            return fetchResult.Result.Scripts;
        }

        public static async Task<Results> SearchScriptsAsync(
            string query,
            int? page = 1,
            int? max = 20,
            ScriptCost? mode = null,
            bool? patched = null,
            bool? key = null,
            bool? universal = null,
            bool? verified = null,
            SortBy? sortBy = null,
            Order? order = null,
            bool? strict = null
        )
        {
            (string Key, string Value)[] queryParams =
            [
                ("q", query),
                ("page", page.InternalClamp(1, MaxValue).ToString()),
                ("max", max.InternalClamp(1, 20).ToString()),
                ("mode", mode.ToString()),
                ("patched", patched.GetBoolInt()),
                ("key", key.GetBoolInt()),
                ("universal", universal.GetBoolInt()),
                ("verified", verified.GetBoolInt()),
                ("sortBy", sortBy.ToString()),
                ("order", order.ToString()),
                ("strict", strict.ToString().ToLower())
            ];

            FetchResult fetchResult = await Client.Get<FetchResult>("script/search", queryParams);

            return fetchResult.Result;
        }

    }
}
