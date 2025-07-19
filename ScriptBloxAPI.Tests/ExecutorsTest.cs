using ScriptBloxApi.Executors;
using ScriptBloxApi.Objects;

namespace ScriptBloxAPI.Tests
{
    public class ExecutorsTests
    {
        [Fact]
        public async Task GetRobloxVersions_ReturnsList()
        {
            IReadOnlyList<Versions>? versions = await Executors.GetRobloxVersions();
            Assert.NotEmpty(versions);
        }

        [Fact]
        public async Task GetExecutorList_ReturnsExecutors()
        {
            IReadOnlyList<Executor>? list = await Executors.GetExecutorList();
            Assert.NotEmpty(list);
        }

        [Fact]
        public async Task GetExecutorInfo_ReturnsExecutorInfo()
        {
            ExecutorInfo? info = await Executors.GetExecutorInfo("Cryptic"); // replace with known valid executor
            Assert.NotNull(info);
            Assert.IsType<ExecutorInfo>(info);
        }
    }
}
