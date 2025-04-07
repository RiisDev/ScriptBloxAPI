using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using ScriptBloxApi.LanguageFeatures;
using ScriptBloxApi.Objects;

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
            Dictionary<string, string> queryParams = new ();

            if (page.HasValue) queryParams.Add("page", page.Value.ToString());
            if (max.HasValue) queryParams.Add("max", max.InternalClamp(1, 20).ToString());
            if (mode.HasValue) queryParams.Add("mode", mode.ToString());
            if (patched.HasValue) queryParams.Add("patched", patched.Value.GetBoolInt());
            if (key.HasValue) queryParams.Add("key", key.Value.GetBoolInt());
            if (universal.HasValue) queryParams.Add("universal", universal.Value.GetBoolInt());
            if (verified.HasValue) queryParams.Add("verified", verified.Value.GetBoolInt());
            if (sortBy.HasValue) queryParams.Add("sortBy", sortBy.ToString());
            if (order.HasValue) queryParams.Add("order", order.ToString());

            FetchResult fetchResult = await Client.Get<FetchResult>("script/fetch", queryParams);

            return fetchResult.Result;
        }

        public static async Task<ScriptData> FetchScriptAsync(string scriptId) => await Client.Get<ScriptData>($"script/{scriptId}", []);

        public static async Task<string> FetchRawScriptAsync(string scriptId) => await Client.Get<string>($"script/raw/{scriptId}", []);

        public static async Task<IReadOnlyList<Script>> FetchTrendingScriptsAsync(int? max = 20)
        {
            FetchResult fetchResult = await Client.Get<FetchResult>("script/trending", new Dictionary<string, string>
            {
                {"max", max.InternalClamp(1, 20).ToString()}
            });
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
            Dictionary<string, string> queryParams = new (){ { "q", query } };

            if (page.HasValue) queryParams.Add("page", page.Value.ToString());
            if (max.HasValue) queryParams.Add("max", max.InternalClamp(1, 20).ToString());
            if (mode.HasValue) queryParams.Add("mode", mode.ToString());
            if (patched.HasValue) queryParams.Add("patched", patched.Value.GetBoolInt());
            if (key.HasValue) queryParams.Add("key", key.Value.GetBoolInt());
            if (universal.HasValue) queryParams.Add("universal", universal.Value.GetBoolInt());
            if (verified.HasValue) queryParams.Add("verified", verified.Value.GetBoolInt());
            if (sortBy.HasValue) queryParams.Add("sortBy", sortBy.ToString());
            if (order.HasValue) queryParams.Add("order", order.ToString());
            if (strict.HasValue) queryParams.Add("strict", strict.ToString().ToLower());
            
            FetchResult fetchResult = await Client.Get<FetchResult>("script/search", queryParams);

            return fetchResult.Result;
        }

    }
}
