using ScriptBloxApi.Objects;
using ScriptBloxApi.Scripts;

namespace ScriptBloxAPI.Tests
{
    public class ScriptsTests
    {

        [Theory]
        // Basic default call
        [InlineData(1, 10, null, null, null, null, null, null, null)]

        // Free scripts, sorted by views descending
        [InlineData(1, 5, (int)Scripts.ScriptCost.free, true, null, null, null, (int)Scripts.SortBy.views, (int)Scripts.Order.desc)]

        // Paid scripts, filtered by key + verified
        [InlineData(2, 15, (int)Scripts.ScriptCost.paid, false, true, true, true, (int)Scripts.SortBy.likeCount, (int)Scripts.Order.asc)]

        // Universal only, sorted by updatedAt
        [InlineData(1, 20, null, null, null, true, null, (int)Scripts.SortBy.updatedAt, (int)Scripts.Order.desc)]

        // Verified scripts, sorted by createdAt ascending
        [InlineData(3, 7, null, null, null, null, true, (int)Scripts.SortBy.createdAt, (int)Scripts.Order.asc)]

        // Filtered by dislike count (low value edge case for page/max)
        [InlineData(1, 1, null, null, null, null, null, (int)Scripts.SortBy.dislikeCount, (int)Scripts.Order.desc)]
        public async Task FetchScriptsAsync_WithVariants_ReturnsResults(
            int page,
            int max,
            int? mode,
            bool? patched,
            bool? key,
            bool? universal,
            bool? verified,
            int? sortBy,
            int? order
        )
        {
            Results? result = await Scripts.FetchScriptsAsync(
                page: page,
                max: max,
                mode: mode.HasValue ? (Scripts.ScriptCost?)mode.Value : null,
                patched: patched,
                key: key,
                universal: universal,
                verified: verified,
                sortBy: sortBy.HasValue ? (Scripts.SortBy?)sortBy.Value : null,
                order: order.HasValue ? (Scripts.Order?)order.Value : null
            );

            Assert.NotNull(result);
            Assert.IsType<Results>(result);
            Assert.True(result.Scripts?.Count >= 0); // May be empty, but should be valid
        }

        [Fact]
        public async Task FetchScriptAsync_ReturnsScriptData()
        {
            ScriptData? script = await Scripts.FetchScriptAsync("61abb678404510c9285337ec"); // https://scriptblox.com/script/GUI_57
            Assert.NotNull(script);
            Assert.IsType<ScriptData>(script);
        }

        [Fact]
        public async Task FetchTrendingScriptsAsync_ReturnsList()
        {
            IReadOnlyList<Script>? scripts = await Scripts.FetchTrendingScriptsAsync();
            Assert.NotEmpty(scripts);
        }

        [Fact]
        public async Task SearchScriptsAsync_ReturnsResults()
        {
            Results? result = await Scripts.SearchScriptsAsync("Infinite Yield");
            Assert.NotNull(result);
            Assert.IsType<Results>(result);
        }
    }
}
