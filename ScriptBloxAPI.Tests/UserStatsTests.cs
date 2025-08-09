using ScriptBloxApi.Objects;
using ScriptBloxApi.User;

namespace ScriptBloxAPI.Tests
{
    public class UserStatsTests
    {
        [Fact]
        public async Task GetUserFollowingAsync_ReturnsResults()
        {
            FollowRoot? result = await UserStats.GetUserFollowingAsync("Septex");
            Assert.NotNull(result);
            Assert.IsType<FollowRoot>(result);
        }

        [Fact]
        public async Task GetUserScriptStatsAsync_ReturnsResults()
        {
            UserScriptsStats? result = await UserStats.GetUserScriptStatsAsync("Septex");
            Assert.NotNull(result);
            Assert.IsType<UserScriptsStats>(result);
        }

        [Fact]
        public async Task GetUserFollowersAsync_ReturnsResults()
        {
            FollowRoot? result = await UserStats.GetUserFollowersAsync("Septex");
            Assert.NotNull(result);
            Assert.IsType<FollowRoot>(result);
        }

        [Fact]
        public async Task GetUserInfoAsync_ReturnsUserInfo()
        {
            UserInfo? result = await UserStats.GetUserInfoAsync("Septex");
            Assert.NotNull(result);
            Assert.IsType<UserInfo>(result);
            Assert.Equal("Septex", result.User.Username);
        }
    }
}
