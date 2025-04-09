using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ScriptBloxApi.Executors
{
    public static class Executors
    {
        public static async Task<IReadOnlyList<Version>> GetRobloxVersions()
        {
            IReadOnlyList<Version> result = await Client.Get<IReadOnlyList<Version>>("roblox/versions", []);
            return result;
        }

        public static async Task<string> GetExecutorList()
        {
            //await Client.Get<IReadOnlyList<Version>>("executor/list", []); They haven't finished it on their end
            return "";
        }
    }
}
