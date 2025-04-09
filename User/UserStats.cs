using System.Threading.Tasks;
using ScriptBloxApi.Objects;

namespace ScriptBloxApi.User
{
    public static class UserStats
    {
        public static async Task<FollowRoot> GetUserFollowingAsync(string username, int? page = 1, int? max = 20)
        {
            (string Key, string Value)[] queryParams =
            [
                ("page", page?.ToString()),
                ("max", max?.ToString())
            ];
            FollowRoot result = await Client.Get<FollowRoot>($"user/{username}/following", queryParams);
            return result;
        }

        public static async Task<FollowRoot> GetUserFollowersAsync(string username, int? page = 1, int? max = 20)
        {
            (string Key, string Value)[] queryParams =
            [
                ("page", page?.ToString()),
                ("max", max?.ToString())
            ];
            FollowRoot result = await Client.Get<FollowRoot>($"user/{username}/followers", queryParams);
            return result;
        }

        public static async Task<UserInfo> GetUserInfoAsync(string username, int? page = 1, int? max = 20)
        {
            UserInfo result = await Client.Get<UserInfo>($"user/info/{username}", []);
            return result;
        }
    }
}
