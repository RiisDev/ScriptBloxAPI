using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ScriptBloxApi.LanguageFeatures;
using ScriptBloxApi.Objects;
using static System.Int32;

namespace ScriptBloxApi.Scripts
{
    public static class Comments
    {
        public static async Task<IReadOnlyList<Comment>> GetScriptCommentsAsync(string scriptId, int? page = 1, int? max = 20)
        {
            CommentRoot result = await Client.Get<CommentRoot>($"comment/{scriptId}", [
                ("page", page.InternalClamp(1, MaxValue).ToString()),
                ("max", max.InternalClamp(1, 20).ToString())
            ]);

            return result.Result.Comments;
        }
    }
}
