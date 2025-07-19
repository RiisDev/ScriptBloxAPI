using System.Collections.Generic;
using System.Threading.Tasks;
using ScriptBloxApi.Objects;

namespace ScriptBloxApi.Executors
{
    public static class Executors
    {
        public static async Task<IReadOnlyList<Versions>> GetRobloxVersions() => await Client.Get<IReadOnlyList<Versions>>("roblox/versions", []);

        public static async Task<IReadOnlyList<Executor>> GetExecutorList() => await Client.Get<IReadOnlyList<Executor>>("executor/list", []);

        public static async Task<ExecutorInfo> GetExecutorInfo(string executor) => await Client.Get<ExecutorInfo>($"executor/{executor}", []);
    }
}
