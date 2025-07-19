using ScriptBloxApi.Objects;
using ScriptBloxApi.Scripts;

namespace ScriptBloxAPI.Tests
{
    public class CommentsTests
    {
        [Fact]
        public async Task GetScriptCommentsAsync_ReturnsList()
        {
            IReadOnlyList<Comment>? comments = await Comments.GetScriptCommentsAsync("61abb678404510c9285337ec"); // https://scriptblox.com/script/GUI_57
            Assert.NotNull(comments);
            Assert.All(comments, c => Assert.IsType<Comment>(c));
        }
    }
}